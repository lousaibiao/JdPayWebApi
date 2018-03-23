using YAXLib;

namespace JdPay.Data.Response
{
    public class FastPayRsp
    {
        
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("TRADE")]
        public TradeFastPayRspTrade Trade { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("RETURN")]
        public TradeFastPayRspReturn Return { get; set; }
    }

    public class TradeFastPayRspTrade
    {
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("TYPE")]
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("ID")]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("AMOUNT")]
        public string Amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("CURRENCY")]
        public string Currency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("DATE")]
        public string Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("TIME")]
        public string Time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("NOTE")]
        public string Note { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("STATUS")]
        public string Status { get; set; }

    }

    public class TradeFastPayRspReturn
    {
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("CODE")]
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("DESC")]
        public string Desc { get; set; }

    }
}