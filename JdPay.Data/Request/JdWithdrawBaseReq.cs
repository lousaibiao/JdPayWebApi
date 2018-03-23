using Newtonsoft.Json;

namespace JdPay.Data.Request
{
    public class JdWithdrawBaseReq
    {
        public JdWithdrawBaseReq(string customerNo)
        {
            this.CustomerNo = customerNo;
        }
        /// <summary>
        /// 提交者会员号	customer_no	yes	String(24)	提交者会员号或企业会员或个人会员
        /// </summary>
        /// <returns></returns>
        [JsonProperty("customer_no")]
        public string CustomerNo { get; set; }
    }
}