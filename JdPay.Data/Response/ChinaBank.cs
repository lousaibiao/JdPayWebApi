using System;
using System.Collections.Generic;
using System.Text;
using YAXLib;

namespace JdPay.Data.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class ChinaBank
    {
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("VERSION")]
        public string Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("MERCHANT")]
        public string Merchant { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("TERMINAL")]
        public string Terminal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("DATA")]
        public string Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [YAXSerializeAs("SIGN")]
        public string Sign { get; set; }

    }
}
