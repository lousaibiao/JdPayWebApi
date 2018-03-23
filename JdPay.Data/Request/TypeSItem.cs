using YAXLib;

namespace JdPay.Data.Request
{
    /// <summary>
    /// 消费
    /// </summary>
    [YAXSerializeAs("DATA")]
    public class TypeSItem
    {
        
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("CARD")]
        public TypeSItemCard Card { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("TRADE")]
        public TypeSItemTrade Trade { get; set; }
        
    }

    public class TypeSItemCard
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
        public string Type => CardType.D.ToString();
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("NO")]
        public string No { get; set; }
        /// <summary>
        /// 信用卡有效期
        /// </summary>
        [YAXSerializeAs("exp")] public string Exp => "";
        /// <summary>
        /// 信用卡校验码
        /// </summary>
        [YAXSerializeAs("cvv2")] public string Cvv2 => "";
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("IDTYPE")]
        public string IdType => "I";
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

    public class TypeSItemTrade
    {
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("TYPE")]
        public string Type => TradeType.S.ToString();
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
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("NOTICE")]

        public string Notice { get; set; }
        
    }
}