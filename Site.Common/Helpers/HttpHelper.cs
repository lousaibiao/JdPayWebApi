using Newtonsoft.Json;
using Site.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Site.Common.Helpers
{
    public static class HttpHelper
    {
        /// <summary>
        /// 获取返回的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        public static async Task<T> GetResultAsync<T>(HttpReqModel reqModel) where T : class
        {
            var rsp = await BuildHttpResponseMessage(reqModel);
            var rspString = await rsp.Content.ReadAsStringAsync();
            try {
                if (typeof(T) == typeof(string)) {
                    return rspString as T;
                }
                return JsonConvert.DeserializeObject<T>(rspString);
            } catch (Exception ex) {
                throw new BizException("读取对象失败", $"序列化{typeof(T)}失败，返回结果为{rspString}，异常：{ex.Message}");
            }
        }

        public static async Task SendRequstAsync(HttpReqModel reqModel)
        {
            await BuildHttpResponseMessage(reqModel);
        }
        private static async Task<HttpResponseMessage> BuildHttpResponseMessage(HttpReqModel reqModel)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            var reqUrl = reqModel.Url;
            using (var httpClient = new HttpClient(handler)) {
                var reqParams = new List<string>();
                if (reqModel.ReqData != null) {
                    foreach (var prop in reqModel.ReqData.GetType().GetProperties()) {
                        var propValue = Convert.ToString(prop.GetValue(reqModel.ReqData));
                        if (!String.IsNullOrEmpty(propValue)) {
                            reqParams.Add($"{prop.Name}={propValue}");
                        }
                    }
                }
                if (reqParams.Count > 0) {
                    reqUrl += ("?" + String.Join("&", reqParams));
                }
                HttpResponseMessage rsp = null;
                foreach (var pair in (reqModel.Headers ?? new Dictionary<string, string>())) {
                    httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
                }
                if (String.Equals(reqModel.Method, "post", StringComparison.OrdinalIgnoreCase)) {
                    HttpContent postContent = null;
                    if (reqModel.ReqBody.GetType() == typeof(Dictionary<string, string>)) {
                        //form-url-encoded
                        postContent = new FormUrlEncodedContent(reqModel.ReqBody as Dictionary<string, string>);
                    } else {
                        postContent = new StringContent(JsonConvert.SerializeObject(reqModel.ReqBody ?? new { }), Encoding.UTF8, "application/json");
                    }
                    rsp = await httpClient.PostAsync(reqUrl, postContent);
                } else if (String.Equals(reqModel.Method, "get", StringComparison.OrdinalIgnoreCase)) {
                    rsp = await httpClient.GetAsync(reqUrl);
                } else {
                    throw new BizException($"请求地址{reqModel.Url}参数有误，只支持Post 和 Get");
                }
                if (rsp.StatusCode != HttpStatusCode.OK) {
                    throw new BizException("模拟请求失败", $"请求失败，参数{JsonConvert.SerializeObject(reqModel)}，返回{rsp.StatusCode}，Body为{await rsp.Content.ReadAsStringAsync()}");
                }
                return rsp;
            }
        }
    }
}
