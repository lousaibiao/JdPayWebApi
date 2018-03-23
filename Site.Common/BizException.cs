using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Common
{
    public class BizException : Exception
    {
        public BizException(string bizMsg, string hiddenMsg = null) : this(0, bizMsg, hiddenMsg) { }
        public BizException(int bizErrorId, string bizMsg, string hiddenMsg = null)
        {
            this.BizMsg = bizMsg;
            this.HiddenMsg = hiddenMsg;
        }
        public string BizMsg { get; set; }
        public string HiddenMsg { get; set; }
        public long BizErrorId { get; set; }
    }
}
