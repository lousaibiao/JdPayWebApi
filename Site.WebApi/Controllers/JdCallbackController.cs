using System.Threading.Tasks;
using JdPay.Data.CallbackModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using Site.Lib.IService;

namespace Site.WebApi.Controllers
{
    /// <summary>
    /// 回调
    /// </summary>
    [Route("api/jdcallback")]
    [Produces("application/json")]
    public class JdCallbackController : Controller
    {
        private readonly ILogger logger;
        private readonly IJdService jdService;
        public JdCallbackController(IJdService jdService)
        {
            this.logger = LogManager.GetLogger("debugInfo");
            this.jdService = jdService;
        }
        /// <summary>
        /// 代付回调地址
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        [HttpPost,Route("defraypay")]
        public async Task<IActionResult> DefrayPay(DefrayPayCallbackQM qm)
        {
            logger.Info($"代付回调返回:{JsonConvert.SerializeObject(qm)}");
            jdService.VerifyDefrayPayCallback(qm);
            //todo 处理代付逻辑
            return Content("ok","text/plain");
        }
        /// <summary>
        /// 快捷回调
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        [HttpPost,Route("expresspay")]
        public async Task<IActionResult> ExpressPay(string resp)
        {
            logger.Info($"快捷回调:{resp}");
            await jdService.ParseFastPayAsync(resp);
            //todo 处理快捷逻辑
            return Content("ok","text/plain");
        }
    }
}