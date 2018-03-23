using Newtonsoft.Json;

namespace JdPay.Data.Response
{
    public class JdWithdrawBaseRsp
    {
        /// <summary>
        /// 提交者会员号	customer_no	yes	String(24)	提交者会员号。或企业会员或个人会员
        /// </summary>
        /// <returns></returns>
        [JsonProperty("customer_no")]
        public string CustomerNo { get; set; }
        /// <summary>
        /// 相应时间	response_datetime	yes	String	格式 yyyymmddTHH24MMSS。例20140820T230000
        /// </summary>
        /// <returns></returns>
        [JsonProperty("response_datetime")]
        public string ResponseDateTime { get; set; }
        /// <summary>
        /// 签名方式	sign_type	yes	String	SHA-256
        /// </summary>
        /// <returns></returns>
        [JsonProperty("sign_type")]
        public string SignType { get; set; }
        /// <summary>
        /// 签名数据	sign_data	yes	String	签名数据
        /// </summary>
        /// <returns></returns>
        [JsonProperty("sign_data")]
        public string SignData { get; set; }
        /// <summary>
        /// 响应编码	response_code	yes	String	返回信息编码表
        /// </summary>
        /// <returns></returns>
        [JsonProperty("response_code")]
        public string ResponseCode { get; set; }
        /// <summary>
        /// 响应描述	response_message	yes	String	返回信息编码表
        /// </summary>
        /// <returns></returns>
        [JsonProperty("response_message")]
        public string ResponseMessage { get; set; }
    }
}