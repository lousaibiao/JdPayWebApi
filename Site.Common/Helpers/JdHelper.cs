using JdPay.Data.Request;
using JdPay.Data.Response;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using SystemX509 = System.Security.Cryptography.X509Certificates;
using SystemX = System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1;
using System.IO;
using Org.BouncyCastle.Pkcs;
using System.Collections;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.X509.Store;
using Site.Common.Models;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using JdPay.Data;
using Microsoft.VisualBasic.CompilerServices;

namespace Site.Common.Helpers
{
    public static class JdHelper
    {
        /// <summary>
        /// pfx文件密码
        /// </summary>
        private const string pfxPwd = "jhw123456";
        /// <summary>
        /// pfx证书，主要是拿私钥
        /// </summary>
        public static string PfxPath => Path.Combine(AppContext.BaseDirectory, "rsa", "jhw123456.pfx");
        /// <summary>
        /// cer证书，拿公钥
        /// </summary>
        public static string CertPath => Path.Combine(AppContext.BaseDirectory, "rsa", "npp_11_API2_pro.cer");
        /// <summary>
        /// YAXLib 不认这串
        /// </summary>
        private const string VERSION_INFO = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        /// <summary>
        /// 不参与签名
        /// </summary>
        public static string[] IGNORES = new String[]
            {"sign_type", "sign_data", "encrypt_type", "encrypt_data", "salt"};
        /// <summary>
        /// 签约的xml
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetTypeVXml(TypeVItem item)
        {
            var card = item.Card;
            var trade = item.Trade;
            return
                $"{VERSION_INFO}<data><card><bank>{card.Bank}</bank><type>{card.Type}</type><no>{card.No}</no><exp>{card.Exp}</exp><cvv2>{card.Cvv2}</cvv2><name>{card.Name}</name><idtype>{card.IdType}</idtype><idno>{card.IdNo}</idno><phone>{card.Phone}</phone></card><trade><type>{trade.Type}</type><id>{trade.Id}</id><amount>{trade.Amount}</amount><currency>{trade.Currency}</currency></trade></data>";
        }
        /// <summary>
        /// 消费的xml
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetTypeSXml(TypeSItem item)
        {
            var card = item.Card;
            var trade = item.Trade;
            var now = DateTime.Now;
            return
                $"{VERSION_INFO}<data><card><bank>{card.Bank}</bank><type>{card.Type}</type><no>{card.No}</no><exp>{card.Exp}</exp><cvv2>{card.Cvv2}</cvv2><name>{card.Name}</name><idtype>{card.IdType}</idtype><idno>{card.IdNo}</idno><phone>{card.Phone}</phone></card><trade><type>{trade.Type}</type><id>{trade.Id}</id><amount>{trade.Amount}</amount><currency>{trade.Currency}</currency><date>{now:yyyyMMdd}</date><time>{now:HHmmss}</time><notice>{trade.Notice}</notice><note></note><code>{trade.Code}</code></trade></data>";
        }

        /// <summary>
        /// des加密
        /// </summary>
        /// <param name="dataXml"></param>
        /// <param name="desKey"></param>
        /// <returns></returns>
        public static string DesEncrypt(string dataXml, string desKey)
        {
            var keyParam = ParameterUtilities.CreateKeyParameter("DES", Convert.FromBase64String(desKey));
            var cipher = (BufferedBlockCipher) CipherUtilities.GetCipher("DES/NONE/PKCS5Padding");

            cipher.Init(true, keyParam);
            var bs = Encoding.UTF8.GetBytes(dataXml);
            var rst = cipher.DoFinal(bs);
            // var asciiBs = Encoding.ASCII.GetBytes(Encoding.UTF8.GetString(rst));
            return Convert.ToBase64String(rst);
        }
        /// <summary>
        /// des解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="desKey"></param>
        /// <returns></returns>
        public static string DesDecrypt(string text, string desKey)
        {
            var keyParam = ParameterUtilities.CreateKeyParameter("DES", Convert.FromBase64String(desKey));
            var cipher = (BufferedBlockCipher) CipherUtilities.GetCipher("DES/NONE/PKCS5Padding");

            cipher.Init(false, keyParam);
            var bs = Convert.FromBase64String(text);
            var rst = cipher.DoFinal(bs);
            return Encoding.UTF8.GetString(rst);
        }
        /// <summary>
        /// md5
        /// </summary>
        /// <param name="version"></param>
        /// <param name="merchant"></param>
        /// <param name="terminal"></param>
        /// <param name="data"></param>
        /// <param name="md5Key"></param>
        /// <returns></returns>
        public static string Md5(string version, string merchant, string terminal, string data, string md5Key)
        {
            var text = $"{version}{merchant}{terminal}{data}{md5Key}";
            using (var md5 = SystemX.MD5.Create())
            {
                var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(bs).Replace("-", "").ToLower();
            }
        }

        public static string CreateChinaBank(string version, string merchant, string terminal, string data, string sign)
        {
            return
                $"{VERSION_INFO}<chinabank><version>{version}</version><merchant>{merchant}</merchant><terminal>{terminal}</terminal><data>{data}</data><sign>{sign}</sign></chinabank>";
        }
        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string Base64Encode(string xml)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
        }
        /// <summary>
        /// base64 解码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Base64Decode(string text)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        }
        /// <summary>
        /// 格式化xml成对象
        /// </summary>
        /// <param name="text"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ParseXml<T>(string text) where T : class
        {
            text = text.Replace(VERSION_INFO, "");
            YAXLib.YAXSerializer serializer = new YAXLib.YAXSerializer(typeof(T));
            var rst = serializer.Deserialize(text) as T;
            return rst;
            //var xmlSeralizer = new XmlSerializer(typeof(ChinaBank));

        }
        /// <summary>
        /// sha256算签名，代付请求的时候用
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ComputeSha256(string text)
        {
            /*
              SHA512Digest digester = new SHA512Digest();
    byte[] retValue = new byte[digester.getDigestSize()];
    digester.update(key.getBytes(), 0, key.length());
    digester.doFinal(retValue, 0);
    return retValue;
             */
            Sha256Digest sha256Digest = new Sha256Digest();
            var retValue = new byte[sha256Digest.GetDigestSize()];
            var bs = Encoding.UTF8.GetBytes(text);
            sha256Digest.BlockUpdate(bs, 0, bs.Length);
            sha256Digest.DoFinal(retValue, 0);
            return BitConverter.ToString(retValue).Replace("-", "");

        }

        /// <summary>
        /// sha1算签名， 代付回调验签
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ComputeSha1(string text)
        {
            Sha1Digest sha1Digest = new Sha1Digest();
            var retValue = new byte[sha1Digest.GetDigestSize()];
            var bs = Encoding.UTF8.GetBytes(text);
            sha1Digest.BlockUpdate(bs, 0, bs.Length);
            sha1Digest.DoFinal(retValue, 0);
            return BitConverter.ToString(retValue).Replace("-", "");

        }
        /// <summary>
        /// 我也不晓得这个叫什么
        /// </summary>
        /// <param name="orgData"></param>
        /// <returns></returns>
        public static string SignEnvelop(string orgData)
        {
            Pkcs12StoreBuilder pkcs12StoreBuilder = new Pkcs12StoreBuilder();
            var pkcs12Store = pkcs12StoreBuilder.Build();
            pkcs12Store.Load(File.OpenRead(PfxPath), pfxPwd.ToCharArray());
            IEnumerable aliases = pkcs12Store.Aliases;
            var enumerator = aliases.GetEnumerator();
            enumerator.MoveNext();
            var alias = enumerator.Current.ToString();
            var privKey = pkcs12Store.GetKey(alias);

            var x509Cert = pkcs12Store.GetCertificate(alias).Certificate;
            var sha1Signer = SignerUtilities.GetSigner("SHA1withRSA");
            sha1Signer.Init(true, privKey.Key);

            var bs = Encoding.UTF8.GetBytes(orgData);

            CmsSignedDataGenerator gen = new CmsSignedDataGenerator();
            gen.AddSignerInfoGenerator(
                new SignerInfoGeneratorBuilder().Build(new Asn1SignatureFactory("SHA1withRSA", privKey.Key), x509Cert));
            IList certList = new ArrayList();
            certList.Add(x509Cert);
            gen.AddCertificates(X509StoreFactory.Create("Certificate/Collection",
                new X509CollectionStoreParameters(certList)));
            var msg = new CmsProcessableByteArray(bs);
            var sigData = gen.Generate(msg, true);
            var signData = sigData.GetEncoded();

            var certificate = DotNetUtilities.FromX509Certificate(new SystemX509.X509Certificate(CertPath));
            var rst = Convert.ToBase64String(EncryptEnvelop(certificate, signData));
            return rst;
        }

        public static async Task<T> GetWithdrawRspAsync<T>(string url, JdWithdrawRequest req) where T : class
        {
            url.NotNull("请求地址");
            var reqBody = new Dictionary<string, string>();
            reqBody.Add("encrypt_type", req.EncryptType);
            reqBody.Add("customer_no", req.CustomerNo);
            reqBody.Add("sign_data", req.SignData);
            reqBody.Add("encrypt_data", req.EncryptData);
            reqBody.Add("sign_type", req.SignTye);
            var httpReqModel = new HttpReqModel()
            {
                Url = url,
                Method = "post",
                ReqBody = reqBody
            };
            return await HttpHelper.GetResultAsync<T>(httpReqModel);
        }

        public static async Task<T> GetWithdrawRspAsync<T>(string url, string customerNo, string sha256SignKey,
            JdWithdrawBaseReq req) where T : class
        {
            var jdReq = BuildJdWithdrawRequest(customerNo, sha256SignKey, req);
            return await GetWithdrawRspAsync<T>(url, jdReq);
        }


        /// <summary>
        /// 获取待签名的内容
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetSignData(JdWithdrawBaseReq req)
        {
            var dic = new Dictionary<string, string>();

            foreach (var prop in req.GetType().GetProperties())
            {
                var attrs = prop.GetCustomAttributes(typeof(JsonPropertyAttribute), false) as JsonPropertyAttribute[];
                if (attrs.Any())
                {
                    var attr = attrs.First();
                    var value = Convert.ToString(prop.GetValue(req));
                    if (!String.IsNullOrEmpty(value) && !IGNORES.Contains(attr.PropertyName))
                    {
                        dic.Add(attr.PropertyName, value);
                    }
                }
            }

            return String.Join("&", dic.OrderBy(w => w.Key).Select(w => $"{w.Key}={w.Value}"));
        }

        public static JdWithdrawRequest BuildJdWithdrawRequest(string customerNo, string sha256SignKey,
            JdWithdrawBaseReq req)
        {
            var srcSignData = JdHelper.GetSignData(req);
            var jdReq = new JdWithdrawRequest(customerNo)
            {
                SignData = JdHelper.ComputeSha256(srcSignData + sha256SignKey),
                EncryptData = JdHelper.SignEnvelop(srcSignData)
            };
            return jdReq;
        }

        private static byte[] EncryptEnvelop(X509Certificate certificate, byte[] bsOrgData)
        {
            var gen = new CmsEnvelopedDataGenerator();
            var data = new CmsProcessableByteArray(bsOrgData);
            gen.AddKeyTransRecipient(certificate);

            var enveloped = gen.Generate(data, CmsEnvelopedDataGenerator.DesEde3Cbc);
            var a = enveloped.ContentInfo.ToAsn1Object();
            return a.GetEncoded();
        }
    }
}
