using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Lib.Model.DomainModel
{
    /// <summary>
    /// 
    /// </summary>
    public class JdConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Merchant { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Terminal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DesKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Md5Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CustomerNo { get; set; }
        /// <summary>
        /// 快捷支付请求地址
        /// </summary>
        public string FastPayUrl { get; set; }
        /// <summary>
        /// 代付请求地址
        /// </summary>
        /// <returns></returns>
        public string DefrayPayUrl { get; set; }
        /// <summary>
        /// 代付回调地址
        /// </summary>
        public string DefrayPayNotifyUrl { get; set; }
        /// <summary>
        /// 快捷回调地址
        /// </summary>
        public string ExpressPayNotifyUrl { get; set; }

        /// <summary>
        /// 交易查询
        /// </summary>
        /// <returns></returns>
        public string TradeQueryUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Sha256SignKey { get; set; }
        


    }
}
