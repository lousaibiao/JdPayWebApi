using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Site.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Site.Common.Helpers;
using System.Net.Http;
using System.IO;

namespace Site.WebApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private const string MSG = "网络异常，请重试";
        private readonly ILogger logger;
        private readonly ILogger bizLogger;
        public ApiExceptionFilterAttribute()
        {
            this.logger = LogManager.GetCurrentClassLogger();
            this.bizLogger = LogManager.GetLogger("bizError");
        }

        protected ApiExceptionFilterAttribute(ILogger logger)
        {
            this.logger = logger;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var reqBody = "";
            var path = context.HttpContext.Request.Path.ToString();
            if (context.HttpContext.Request.Method == HttpMethod.Post.ToString())
            {
                if (context.HttpContext.Request.Body.CanSeek)
                {
                    context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                    using (var sr = new StreamReader(context.HttpContext.Request.Body))
                    {
                        reqBody = await sr.ReadToEndAsync();
                        reqBody = reqBody.Replace("\r", "").Replace("\n", "");
                    }
                }
            }

            context.ExceptionHandled = true;
            switch (context.Exception)
            {
                //返回的结果模拟正常的结果，给不同的code就好了。
                case BizException bizError:
                    context.Result = new ContentResult()
                    {
                        Content = new {response_code = 999400, response_message = bizError.BizMsg}.ToJsonString(),
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                    bizLogger.Info(bizError,
                        () => $"业务异常，地址{path},参数{reqBody},异常信息: {bizError.BizMsg},{bizError.HiddenMsg}");
                    break;
                case DbUpdateException dbError:
                    context.Result = new ContentResult()
                    {
                        Content = new {response_code = 999500, response_message ="jd支付系统异常" }.ToJsonString(),
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                    logger.Error(dbError,
                        () =>
                            $"数据库异常,地址{path},参数{reqBody},异常信息:{dbError.Message},{dbError.InnerException?.Message},{dbError.StackTrace}");
                    break;
                default:
                    var ex = context.Exception;
                    context.Result = new ContentResult()
                    {
                        Content = new {response_code = 999500, response_message = "jd支付系统异常"}.ToJsonString(),
                        ContentType = "applicatio/json",
                        StatusCode = 200
                    };
                    logger.Error(ex, () => $"系统异常，地址{path},参数{reqBody},异常信息:{ex.Message},{ex.StackTrace}");
                    break;
            }

        }
    }
}
