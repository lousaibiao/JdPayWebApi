using System;

namespace Site.Common
{
    public class TokenExpireException:Exception
    {
        public string TokenExpireMsg { get; set; }
        public TokenExpireException():this("登录超时啦")
        {
            
        }
        public TokenExpireException(string tokenExpireMsg)
        {
            this.TokenExpireMsg = tokenExpireMsg;
        }
    }
}