using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Site.WebApi.Filters
{
    public class JdPayAuthActionFilter : IActionFilter
    {
        private const string TokenKey = "JdPayAuthToken";
        public string Token { get; set; }

        public JdPayAuthActionFilter(string authToken)
        {
            this.Token = authToken;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //代付的请求做验证
            if (context.HttpContext.Request.Path.Value.ToLower()
                    .IndexOf("jd/defraypay", StringComparison.CurrentCulture) == -1) return;
            var contains = context.HttpContext.Request.Headers.TryGetValue(TokenKey, out StringValues token);
            if (!contains || token != Token)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonConvert.SerializeObject(new
                    {
                        response_code = 999401,
                        response_message = "京东支付：非法请求！"
                    })
                };
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
//            throw new System.NotImplementedException();
        }
    }
}