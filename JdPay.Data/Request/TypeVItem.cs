using System;
using System.Collections.Generic;
using System.Text;
using YAXLib;

namespace JdPay.Data.Request
{
    /*
     public class Warehouse
{
    [YAXAttributeForClass()]
    public string Name { get; set; }

    [YAXSerializeAs("address")]
    [YAXAttributeFor("SiteInfo")]
    public string Address { get; set; }

    [YAXSerializeAs("SurfaceArea")]
    [YAXElementFor("SiteInfo")]
    public double Area { get; set; }
}

<Warehouse Name="Foo Warehousing Ltd.">
  <SiteInfo address="No. 10, Some Ave., Some City, Some Country">
    <SurfaceArea>120000.5</SurfaceArea>
  </SiteInfo>
</Warehouse>
         */
    /// <summary>
    /// 快捷支付签约
    /// </summary>
    [YAXSerializeAs("data")]
    public class TypeVItem
    {
        [YAXSerializeAs("card")]
        public TypeVCard Card { get; set; }
        [YAXSerializeAs("trade")]
        public TypeVTrade Trade { get; set; }
    }

    public class TypeVCard
    {
        [YAXSerializeAs("bank")]
        public string Bank { get; set; }

        /// <summary>
        /// 信用卡：C / 借记卡：D
        /// </summary>
        [YAXSerializeAs("type")]
        public CardType Type => CardType.D;
        [YAXSerializeAs("no")]
        public string No { get; set; }
        /// <summary>
        /// 信用卡有效期
        /// </summary>
        [YAXSerializeAs("exp")] public string Exp => "";
        /// <summary>
        /// 信用卡校验码
        /// </summary>
        [YAXSerializeAs("cvv2")] public string Cvv2 => "";
        [YAXSerializeAs("name")]
        public string Name { get; set; }
        [YAXSerializeAs("idtype")]
        public string IdType => "I";
        [YAXSerializeAs("idno")]
        public string IdNo { get; set; }
        [YAXSerializeAs("phone")]
        public string Phone { get; set; }
    }
    public class TypeVTrade
    {
        [YAXSerializeAs("type")]
        public string Type => TradeType.V.ToString();
        [YAXSerializeAs("id")]
        public string Id { get; set; }
        [YAXSerializeAs("amount")]
        public string Amount { get; set; }
        [YAXSerializeAs("currency")]
        public string Currency => "CNY";

    }
}
