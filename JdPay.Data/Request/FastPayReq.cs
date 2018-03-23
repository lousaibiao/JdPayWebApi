using YAXLib;

namespace JdPay.Data.Request
{
    [YAXSerializeAs("DATA")]
    public class FastPayReq
    {
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("CARD")]
        public FastPayReqCard Card { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("TRADE")]
        public FastPayReqTrade Trade { get; set; }
    }

   public  class FastPayReqCard
    {
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("BANK")]
        public string Bank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("TYPE")]
        public CardType Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("NO")]
        public string No { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("NAME")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("IDTYPE")]
        public string IdType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("IDNO")]
        public string IdNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("PHONE")]
        public string Phone { get; set; }
    }

    public class FastPayReqTrade
    {
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("TYPE")]
        public string Type => "S";
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
        public string Currency => "CNY";
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("CODE")]
        public string Code { get; set; }
        
    }
}