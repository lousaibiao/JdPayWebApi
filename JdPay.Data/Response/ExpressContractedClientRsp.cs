using System;
using System.Collections.Generic;
using System.Text;
using YAXLib;

namespace JdPay.Data.Response
{
    /*
     <?xml version="1.0" encoding="UTF-8"?><DATA>  <TRADE>    <TYPE>V</TYPE>    <ID/>    <AMOUNT/>    <CURRENCY/>  </TRADE>  <RETURN>    <CODE>EEN0015</CODE>    <DESC>交易已成功，请勿重复支付</DESC>  </RETURN></DATA>
         */
    [YAXSerializeAs("DATA")]
    public class ExpressContractedClientRsp
    {
        [YAXSerializeAs("TRADE")]
        public ExpressContractedClientTrade Trade { get; set; }
        [YAXSerializeAs("RETURN")]
        public ExpressContractedClientReturn Return { get; set; }

    }
    public class ExpressContractedClientTrade
    {
        [YAXSerializeAs("TYPE")]
        public string Type { get; set; }
        [YAXSerializeAs("ID")]
        public string Id { get; set; }
        [YAXSerializeAs("AMOUNT")]
        public string Amount { get; set; }
        [YAXSerializeAs("CURRENCY")]
        public string Currency { get; set; }
    }
    public class ExpressContractedClientReturn
    {
        [YAXSerializeAs("CODE")]
        public string Code { get; set; }
        [YAXSerializeAs("DESC")]
        public string Desc { get; set; }

    }
}
