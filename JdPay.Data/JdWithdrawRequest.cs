using Newtonsoft.Json;

namespace JdPay.Data
{
    /// <summary>
    /// 代付接口发送给jd的请求参数
    /// </summary>
    public class JdWithdrawRequest
    {
        public JdWithdrawRequest(string CustomerNo)
        {
            this.CustomerNo = CustomerNo;
        }
        /// <summary>
        /// 提交者会员号
        /// </summary>
        /// <returns></returns>
        [JsonProperty("customer_no")]
        public string CustomerNo { get; set; }
        /// <summary>
        /// 签名方式
        /// </summary>
        /// <returns></returns>
        [JsonProperty("sign_type")]
        public string SignTye => "SHA-256";
        /// <summary>
        /// 签名数据
        /// </summary>
        /// <returns></returns>
        [JsonProperty("sign_data")]
        public string SignData { get; set; }
        /// <summary>
        /// RSA
        /// </summary>
        /// <returns></returns>
        [JsonProperty("encrypt_type")]
        public string EncryptType =>"RSA";
        /// <summary>
        /// 密文数据数据需要按参数名的字母顺序拼接，所有数据使用 “参数名=值&参数名=值”拼接后加密
        /// </summary>
        /// <returns></returns>
        [JsonProperty("encrypt_data")]
        public string EncryptData { get; set; }
    }
}