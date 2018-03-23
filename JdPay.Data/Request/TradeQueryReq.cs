using System;
using Newtonsoft.Json;

namespace JdPay.Data.Request
{
    public class TradeQueryReq : JdWithdrawBaseReq
    {
        public TradeQueryReq():base(String.Empty)
        {
            
        }
        public TradeQueryReq(string customerNo) : base(customerNo)
        {
        }

        /// <summary>
        /// 请求时间	request_datetime	yes	String	格式 yyyymmddTHH24MMSS。例20140820T230000
        /// </summary>
//        [JsonProperty("request_datetime")]
//        public string RequestDateTime => $"{DateTime.Now:yyyyMMddHHmmss}";

        /// <summary>
        /// 商户订单流水号	out_trade_no	yes	String(64)	商户订单号和交易号二者必须有一个有值
        /// </summary>
        /// <returns></returns>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 交易号	trade_no	yes	String(64)	商户订单号和交易号二者必须有一个有值
        /// </summary>
        /// <returns></returns>
        // [JsonProperty("trade_no")]
        // public string TradeNo { get; set; }
        /// <summary>
        /// 查询类型	trade_type	yes	String	固定值 T_AGD
        /// </summary>
        /// <returns></returns>
//        [JsonProperty("trade_type")]
//        public string TradeType=>"T_AGD";
    }
}