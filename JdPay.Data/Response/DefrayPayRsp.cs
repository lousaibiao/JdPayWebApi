using Newtonsoft.Json;

namespace JdPay.Data.Response
{
    public class DefrayPayRsp : JdWithdrawBaseRsp
    {

        /// <summary>
        /// 商户订单流水号	out_trade_no	yes	String(64)	
        /// </summary>
        /// <returns></returns>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 交易号	trade_no	No	String(64)	网银生成
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_no")]
        public string TradeNo { get; set; }
        /// <summary>
        /// 交易状态	trade_status	No	String	
        /// BUID    交易建立
        /// ACSU    已受理
        /// WPAR    等待支付结果
        /// FINI     交易成功
        /// REFU     交易退款
        /// CLOS   交易关闭，失败
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_status")]
        public string TradeStatus { get; set; }
        /// <summary>
        /// 订单交易金额	trade_amount	No	String	单位：分
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_amount")]
        public string TradeAmount { get; set; }
        /// <summary>
        /// 币种	trade_currency	No	String	货币类型，固定填CNY
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_currency")]
        public string TradeCurrency { get; set; }
        /// <summary>
        /// 交易回传参数	return_params	No	String	
        /// </summary>
        /// <returns></returns>
        [JsonProperty("return_params")]
        public string ReturnParams { get; set; }

    }
}