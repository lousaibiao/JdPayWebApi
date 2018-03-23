using System.Threading.Tasks;
using JdPay.Data.Request;
using JdPay.Data.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NLog;
using Site.Lib.IService;
using Site.Lib.Model.DomainModel;

namespace Site.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/jd")]
    public class JdController : Controller
    {
        private readonly ILogger logger = LogManager.GetLogger("debugInfo");

        private readonly IJdService jdService;

        private readonly JdConfig jdConfig;

        public JdController(IJdService jdService, IOptions<JdConfig> jdConfigOpt)
        {
            this.jdService = jdService;
            this.jdConfig = jdConfigOpt.Value;
        }

        /// <summary>
        /// 代付
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost, Route(("DefrayPay"))]
        [ProducesResponseType(typeof(DefrayPayRsp), 200)]
        public async Task<IActionResult> DefrayPayAsync([FromBody] DefrayPayReq req)
        {
            req.CustomerNo = jdConfig.CustomerNo;
            req.NotifyUrl = jdConfig.DefrayPayNotifyUrl;
            var rst = await jdService.DefrayPayAsync(req);
            return Ok(rst);
        }

        /// <summary>
        /// 查询订单结果
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet, Route("TradeQuery")]
        [ProducesResponseType(typeof(TradeQueryRsp), 200)]
        public async Task<IActionResult> TradeQueryAsync([FromQuery] TradeQueryReq req)
        {
            req.CustomerNo = jdConfig.CustomerNo;
            var rst = await jdService.TradeQueryAsync(req);
            return Ok(rst);
        }

        /// <summary>
        /// 快捷支付签约
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>

        [HttpPost, Route("ExpressContract")]
        [ProducesResponseType(typeof(ExpressContractedClientRsp), 200)]
        public async Task<IActionResult> ExpressContractAsync([FromBody] TypeVItem req)
        {
            var rsp = await jdService.ExpressContractAsync(req);
            return Ok(rsp);
        }
        /// <summary>
        /// 快捷支付
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost, Route("FastPay")]
        [ProducesResponseType(typeof(FastPayRsp),200)]
        public async Task<IActionResult> FastPayAsync([FromBody] TypeSItem req)
        {
            var rsp = await jdService.FastPayAsync(req);
            return Ok(rsp);
        }

    }
}