
using System;
using Newtonsoft.Json;

namespace JdPay.Data.Request
{
    /// <summary>
    /// 业务数据 以下数据根据要求签名后数据放入sign_data中，加密后数据放入enctypt_data中
    /// </summary>
    public class DefrayPayReq : JdWithdrawBaseReq
    {
        public DefrayPayReq(string customerNo) : base(customerNo)
        {
        }
        
        /// <summary>
        /// 请求时间 格式 yyyymmddTHH24MMSS。例20140820T230000
        /// </summary>
        /// <returns></returns>
        [JsonProperty("request_datetime")]
        public string RequestDateTime => $"{DateTime.Now:yyyyMMddHHmmss}";
        // public string RequestDateTime { get; set; }
        /// <summary>
        /// 商户订单流水号 商户生成，不允许号码重复
        /// </summary>
        /// <returns></returns>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 业务订单流水号 标示一个业务订单，例如京东的购物编号
        /// </summary>
        /// <returns></returns>
        [JsonProperty("biz_trade_no")]
        public string BizTradeNo { get; set; }
        /// <summary>
        /// 外部订单日 如果为空则从请求时间中获取，格式 yyyymmddTHH24MMSS。例20140820T230000
        /// </summary>
        /// <returns></returns>
        [JsonProperty("out_trade_date")]
        public string OutTradeDate => $"{DateTime.Now:yyyyMMddHHmmss}";
        /// <summary>
        /// 订单交易金额 单位：分，大于0。
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_amount")]
        public string TradeAmount { get; set; }
        /// <summary>
        /// 币种 货币类型，固定CNY
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_currency")]
        public string TradeCurrency => "CNY";
        /// <summary>
        /// 卖方信息 json格式字符串
        /// </summary>
        /// <returns></returns>
        [JsonProperty("seller_info")]
        public string SellerInfo { get; set; }
        /// <summary>
        /// 订单摘要 商品描述，订单标题，关键描述信息
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_subject")]
        public string TradeSubject { get; set; }
        /// <summary>
        /// 商户售卖的商品分类码
        /// </summary>
        /// <returns></returns>
        [JsonProperty("category_code")]
        public string CategoryCode { get; set; }
        /// <summary>
        /// 支付工具，固定值ACCT，XJK，TRAN任意一种。ACCT代付到余额；XJK代付到小金库；TRAN代付到银行卡。
        /// </summary>
        /// <returns></returns>
        [JsonProperty("pay_tool")]
        public string PayTool => "TRAN";
        /// <summary>
        /// 提交业务渠道 pc / app
        /// </summary>
        /// <returns></returns>
        [JsonProperty("trade_source")]
        public string TradeSource => "APP";
        /// <summary>
        /// 收款银行编码
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_bank_code")]
        public string PayeeBankCode { get; set; }
        /// <summary>
        /// 收款联行号
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_bank_associated_code")]
        public string PayeeBankAssociatedCode { get; set; }
        /// <summary>
        /// 收款银行全称
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_bank_fullname")]
        public string PayeeBankFullName { get; set; }
        /// <summary>
        /// 收款行所在国家的地区码
        /// ISO-3166，例如CHN中国，HKG香港，JPN日本，USA美国。默认CHN
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_bank_country_code")]
        public string PayeeBankCountryCode { get; set; }
        /// <summary>
        /// 收款行所在省编码
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_bank_province_code")]
        public string PayeeBankProvinceCode { get; set; }
        /// <summary>
        /// 收款行所在市编码
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_bank_city_code")]
        public string PayeeBankCityCode { get; set; }
        /// <summary>
        /// 收款卡种 支付工具是TRAN时必填
        /// 卡类型 借记卡=DE；信用卡=CR
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_card_type")]
        public string PayeeCardType => "DE";
        /// <summary>
        /// 收款帐户类型	payee_account_type	No	String8	支付工具是TRAN时必填帐户类型，对私户=P；对公户=C
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_account_type")]
        public PayeeAccountType PayeeAccountType { get; set; }
        /// <summary>
        /// 收款帐户号	payee_account_no	No	string 32	支付工具是TRAN时必填
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_account_no")]
        public string PayeeAccountNo { get; set; }
        /// <summary>
        /// 收款帐户名称	payee_account_name	No	string 256	支付工具是TRAN时必填
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_account_name")]
        public string PayeeAccountName { get; set; }
        /// <summary>
        /// 持卡人证件类型	payee_id_type	No	String	身份证=ID
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_id_type")]
        public string PayeeIdType { get; set; } //=>"ID";
        /// <summary>
        /// 持卡人证件号
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_id_no")]
        public string PayeeIdNo { get; set; }
        /// <summary>
        /// 持卡人手机
        /// </summary>
        /// <returns></returns>
        [JsonProperty("payee_mobile")]
        public string PayeeMobile { get; set; }
        /// <summary>
        /// 服务器通知地址	notify_url	No	string 256	网银在线付款成功，付款失败后异步通知地址。
        /// </summary>
        /// <returns></returns>
        [JsonProperty("notify_url")]
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 交易回传参数	return_params	No	string 256
        /// </summary>
        /// <returns></returns>
        [JsonProperty("return_params")]
        public string ReturnParams { get; set; }
        /// <summary>
        /// 扩展参数	extend_params	No	string 1024
        /// </summary>
        /// <returns></returns>
        [JsonProperty("extend_params")]
        public string ExtendParams { get; set; }
        /// <summary>
        /// 银行卡信息类型	bank_card_info_type	No	String	
        /// 卡详细信息 = CARD_INFO  (注：此处不填写，默认卡详细信息代付)卡ID代付 = CARD_ID
        /// </summary>
        /// <returns></returns>
        [JsonProperty("bank_card_info_type")]
        public string BankCardInfoType { get; set; }
        /// <summary>
        /// 卡ID	bank_card_id	No	String 20	卡id值
        /// </summary>
        /// <returns></returns>
        [JsonProperty("bank_card_id")]
        public string BankCardId { get; set; }
    }
}