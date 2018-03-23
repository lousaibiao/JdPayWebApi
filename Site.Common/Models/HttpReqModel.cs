using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Site.Common.Models
{
    public class HttpReqModel
    {
        public String Url { get; set; }
        public object ReqData { get; set; }
        public object ReqBody { get; set; }
        public String Method { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}
