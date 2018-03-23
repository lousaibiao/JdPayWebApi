using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Site.Common.Helpers
{
    public static class StringHelper
    {
        public static string GetMd5String(this string str)
        {
            if (String.IsNullOrEmpty(str)) {
                throw new BizException("字符串不能为空");
            }
            using (var md5 = MD5.Create()) {
                var rst = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                return BitConverter.ToString(rst).Replace("-", "").ToUpper();
            }
        }
        public static string ToJsonString(this object obj)
        {
            if (obj == null) {
                throw new BizException("序列化成json的对象不能为空");
            }
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { MaxDepth = 3 });
        }
    }
}
