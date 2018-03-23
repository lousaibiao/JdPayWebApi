using Newtonsoft.Json;
namespace JdPay.Data.Response
{
    public class TradeQueryRsp : JdWithdrawBaseRsp
    {
        /// <summary>
        /// 商户订单流水号	out_trade_no	Yes	string 64	商户生成，网银侧以此号码重复支付的判断
        /// </summary>
        /// <returns></returns>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 业务订单流水号	biz_trade_no	Yes	string 64	标示一个业务订单，例如京东的购物编号
        /// </summary>
        /// <returns></returns>
        [JsonProperty("biz_trade_no")]
        public string BizTradeNo { get; set; }
        /// <summary>
        /// 外部交易日	out_trade_date	Yes	String	格式 yyyymmddTHH24MMSS。例20140820T230000
        /// </summary>
        /// <returns></returns>
        [JsonProperty("out_trade_date")]
        public string OutTradeDate { get; set; }
        /// <summary>
        /// 交易类别	trade_class	Yes	string 16	
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_class")]
        public string TradeClass { get; set; }
        /// <summary>
        /// 订单摘要	trade_subject	Yes	string 100	商品描述，订单标题，关键描述信息
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_subject")]
        public string TradeSubject { get; set; }
        /// <summary>
        /// 交易号	trade_no	Yes	string 64	网银生成
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_no")]
        public string TradeNo { get; set; }
        /// <summary>
        /// 卖方信息	seller_info	No	String	没有就不返回
        /// </summary>
        /// <returns></returns>
        [JsonProperty("seller_info")]
        public string SellerInfo { get; set; }
        /// <summary>
        /// 买方信息	buyer_info	No	String	没有就不返回
        /// </summary>
        /// <returns></returns>
        [JsonProperty("buyer_info")]
        public string BuyerInfo { get; set; }
        /// <summary>
        /// 订单交易金额	trade_amount	Yes	money	单位：分
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_amount")]
        public string TradeAmount { get; set; }
        /// <summary>
        /// 订单交易币种	trade_currency	Yes	string 8	ISO 4217
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_currency")]
        public string TradeCurrency { get; set; }
        /// <summary>
        /// 已退款金额	refund_amount	No	String	　
        /// </summary>
        /// <returns></returns>
        [JsonProperty("refund_amount")]
        public string RefundAmount { get; set; }
        /// <summary>
        /// 商户售卖的商品分类码	category_code	No	string 16	
        /// </summary>
        /// <returns></returns>
        [JsonProperty("category_code")]
        public string CategoryCode { get; set; }
        /// <summary>
        /// 已确认金额	confirm_amount	No	String	　
        /// </summary>
        /// <returns></returns>
        [JsonProperty("confirm_amount")]
        public string ConfirmAmount { get; set; }
        /// <summary>
        /// 交易响应码	trade_respcode	Yes	　String	　
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_respcode")]
        public string TradeRespCode { get; set; }
        /// <summary>
        /// 交易响应描述	trade_respmsg	NO	String	
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_respmsg")]
        public string TradeRespMsg { get; set; }
        /// <summary>
        /// 交易状态	trade_status	Yes	string 16	交易返回状态编码表
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_status")]
        public string TradeStatus { get; set; }
        /// <summary>
        /// 支付完成日	trade_pay_date	No	String	支付成功后提供
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_pay_date")]
        public string TradePayDate { get; set; }
        /// <summary>
        /// 支付完时间	trade_pay_time	No	String	　
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_pay_time")]
        public string TradePayTime { get; set; }
        /// <summary>
        /// 支付工具	pay_tool	No	string 8	
        /// </summary>
        /// <returns></returns>
        [JsonProperty("pay_tool")]
        public string PayTool { get; set; }
        /// <summary>
        /// 支付银行	bank_code	No	string 8	发卡银行简码。网银帐户支付无银行
        /// </summary>
        /// <returns></returns>
        [JsonProperty("bank_code")]
        public string BankCode { get; set; }
        /// <summary>
        /// 支付卡种	card_type	No	string 8	卡类型， DE借记卡， CR贷记卡， 默认贷记卡
        /// </summary>
        /// <returns></returns>
        [JsonProperty("card_type")]
        public string CardType { get; set; }
        /// <summary>
        /// 交易完成日	trade_finish_date	No	String	交易成功后有
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_finish_date")]
        public string TradeFinishDate { get; set; }
        /// <summary>
        /// 交易完时间	trade_finish_time	No	String	　
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_finish_time")]
        public string TradeFinishTime { get; set; }
        /// <summary>
        /// 订单返回信息	return_params	No	string 256	返回、通知或查询时会原样返回
        /// </summary>
        /// <returns></returns>
        [JsonProperty("return_params")]
        public string ReturnParams { get; set; }

    }
}