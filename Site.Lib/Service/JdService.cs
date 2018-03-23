using JdPay.Data;
using JdPay.Data.Request;
using JdPay.Data.Response;
using Microsoft.Extensions.Options;
using Site.Common;
using Site.Common.Helpers;
using Site.Common.Models;
using Site.Lib.IService;
using Site.Lib.Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JdPay.Data.CallbackModel;
using NLog;

namespace Site.Lib.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class JdService : IJdService
    {
        private readonly JdConfig jdConfig;
        private readonly ILogger debugInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jdConfigOpt"></param>
        public JdService(IOptions<JdConfig> jdConfigOpt)
        {
            this.jdConfig = jdConfigOpt.Value;
            this.debugInfo = LogManager.GetLogger("debugInfo");
        }

        /// <summary>
        /// 参数要判断response_code，然后判断trade_status状态是否成功。
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<DefrayPayRsp> DefrayPayAsync(DefrayPayReq req)
        {
            req.NotNull("请求参数");
            debugInfo.Info($"代付请求:{req.ToJsonString()}");
            req.CustomerNo.NotNull("客户号");
            req.OutTradeNo.NotNull("商户订单流水号");
            req.TradeAmount.NotNull("订单交易金额");
            if (!decimal.TryParse(req.TradeAmount, out decimal tradeAmount))
            {
                throw new BizException("金额不合法");
            }

            tradeAmount.ShouldLargeThan(0, "订单交易金额");
            req.TradeSubject.NotNull("订单摘要");
            if (req.PayTool == PayTool.ACCT.ToString())
            {
                req.PayeeBankCode.NotNull("收款银行编码");
                req.PayeeCardType.NotNull("收款卡种");
                req.PayeeAccountType.EnumValueValid("收款账户类型");
                req.PayeeAccountNo.NotNull("收款账户号");
                req.PayeeAccountName.NotNull("收款账户名称");
            }

            var rst = await JdHelper.GetWithdrawRspAsync<DefrayPayRsp>(jdConfig.DefrayPayUrl, jdConfig.CustomerNo,
                jdConfig.Sha256SignKey, req);
            return rst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<string> Test(string req)
        {
            var rst = await HttpHelper.GetResultAsync<string>(new HttpReqModel()
            {
                Method = "post",
                ReqBody = new Dictionary<string, string>()
                {
                    {"req", req},
                    {"charset", "utf-8"}
                },
                Url = "https://quick.chinabank.com.cn/express.htm"
            });
            return rst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<TradeQueryRsp> TradeQueryAsync(TradeQueryReq req)
        {
            req.NotNull("请求参数");
            debugInfo.Info($"交易查询:{req.ToJsonString()}");
            req.CustomerNo.NotNull("提交者会员号");
            req.OutTradeNo.NotNull("商户订单号");
            var reqDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var dic = new Dictionary<string, string>()
            {
                {"customer_no", jdConfig.CustomerNo},
                {"out_trade_no", req.OutTradeNo},
                {"request_datetime", reqDateTime}
            };
            dic.Add("sign_type", "SHA-256");
            var signSrcTxt =
                $"customer_no={jdConfig.CustomerNo}&out_trade_no={req.OutTradeNo}&request_datetime={reqDateTime}{jdConfig.Sha256SignKey}";
            var sign = JdHelper.ComputeSha256(signSrcTxt);
            dic.Add("sign_data", sign);
            var rsp = await HttpHelper.GetResultAsync<TradeQueryRsp>(new HttpReqModel()
            {
                Url = jdConfig.TradeQueryUrl,
                Method = "post",
                ReqBody = dic
            });
            debugInfo.Info($"交易查询返回：{rsp.ToJsonString()}");
            return rsp;
        }

        /// <summary>
        /// 快捷支付签约
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="BizException"></exception>

        public async Task<ExpressContractedClientRsp> ExpressContractAsync(TypeVItem req)
        {
            req.NotNull("请求参数");
            debugInfo.Info($"快捷签约:{req.ToJsonString()}");
            req.Card.NotNull("卡信息");
            req.Trade.NotNull("交易信息");
            var card = req.Card;
            card.Bank.NotNull("持卡人支付卡号发卡行");
            card.Type.EnumValueValid("持卡人支付卡号卡类型");
            card.No.NotNull("持卡人支付卡号");
            card.Name.NotNull("持卡人姓名");
            card.IdType.NotNull("持卡人证件类型");
            card.IdNo.NotNull("持卡人证件号");
            card.Phone.NotNull("持卡人手机号");

            var trade = req.Trade;
            trade.Type.ShouldBe("V", "交易类型");
            trade.Id.NotNull("交易号");
            trade.Id.NotTooLong(30, "交易号");
            trade.Amount.DecimalValid("交易金额");
            trade.Currency.ShouldBe("CNY", "交易币种");

            var xmlData = JdHelper.GetTypeVXml(req);
            var desEncrypted = JdHelper.DesEncrypt(xmlData, jdConfig.DesKey);
            var sign = JdHelper.Md5(jdConfig.Version, jdConfig.Merchant, jdConfig.Terminal, desEncrypted,
                jdConfig.Md5Key);
            var xml = JdHelper.CreateChinaBank(jdConfig.Version, jdConfig.Merchant, jdConfig.Terminal, desEncrypted,
                sign);
            var base64EncodedStr = JdHelper.Base64Encode(xml);
            var reqModel = new HttpReqModel()
            {
                Method = "post",
                ReqBody = new Dictionary<string, string>()
                {
                    {"req", base64EncodedStr},
                    {"charset", "utf-8"}
                },
                Url = jdConfig.FastPayUrl
            };
            var rspStr = await HttpHelper.GetResultAsync<string>(reqModel);
            var content = rspStr.Substring(rspStr.IndexOf('=') + 1);
            debugInfo.Info($"快捷签约返回原文:{rspStr}");
            var rsp = await ParseExpresssContractAsync(content);
            debugInfo.Info($"快捷签约解析：{rsp.ToJsonString()}");
            return rsp;
        }

        /// <summary>
        /// 快捷支付
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<FastPayRsp> FastPayAsync(TypeSItem req)
        {
            req.NotNull("请求参数");
            debugInfo.Info($"快捷支付:{req.ToJsonString()}");
            var trade = req.Trade;
            trade.NotNull("交易参数");
            trade.Notice = jdConfig.ExpressPayNotifyUrl;
            trade.Type.ShouldBe("S", "交易类型应为S");
            trade.Id.NotNull("交易号");
            trade.Amount.LongValid("金额");
            trade.Code.NotNull("交易验证码");
            trade.Currency.NotNull("交易币种");
            var card = req.Card;
            card.NotNull("卡片信息");
            card.Bank.NotNull("持卡人支付卡号发卡行");
            card.Type.ShouldBe("D", "持卡人卡类型");
            card.No.NotNull("持卡人支付卡号");
            card.Name.NotNull("持卡人姓名");
            card.IdType.NotNull("持卡人证件类型");
            card.IdNo.NotNull("持卡人证件号");
            card.Phone.NotNull("持卡人手机号");
            var typeSXml = JdHelper.GetTypeSXml(req);
            var desEncrypted = JdHelper.DesEncrypt(typeSXml, jdConfig.DesKey);
            var sign = JdHelper.Md5(jdConfig.Version, jdConfig.Merchant, jdConfig.Terminal, desEncrypted,
                jdConfig.Md5Key);
            var chinabank = JdHelper.CreateChinaBank(jdConfig.Version, jdConfig.Merchant, jdConfig.Terminal,
                desEncrypted, sign);
            var base64Encoded = JdHelper.Base64Encode(chinabank);
            var reqModel = new HttpReqModel()
            {
                Method = "post",
                ReqBody = new Dictionary<string, string>()
                {
                    {"req", base64Encoded},
                    {"charset", "utf-8"}
                },
                Url = jdConfig.FastPayUrl
            };
            var rspStr = await HttpHelper.GetResultAsync<string>(reqModel);
            var content = rspStr.Substring(rspStr.IndexOf('=') + 1);
            var rsp = await ParseFastPayAsync(content);
            return rsp;
        }

        /// <summary>
        /// 快捷签约回调
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <exception cref="BizException"></exception>
        public async Task<ExpressContractedClientRsp> ParseExpresssContractAsync(string content)
        {
            var decodedXml = JdHelper.Base64Decode(content);
            var chinaBank = JdHelper.ParseXml<ChinaBank>(decodedXml);
            var exceptedSign = JdHelper.Md5(jdConfig.Version, jdConfig.Merchant, jdConfig.Terminal, chinaBank.Data,
                jdConfig.Md5Key);
            if (exceptedSign != chinaBank.Sign)
            {
                throw new BizException("返回结果签名不正确");
            }

            var desDecrypted = JdHelper.DesDecrypt(chinaBank.Data, jdConfig.DesKey);
            return JdHelper.ParseXml<ExpressContractedClientRsp>(desDecrypted);
        }

        public async Task<FastPayRsp> ParseFastPayAsync(string content)
        {
            var decodedXml = JdHelper.Base64Decode(content);
            var chinaBank = JdHelper.ParseXml<ChinaBank>(decodedXml);
            var exceptedSign = JdHelper.Md5(jdConfig.Version, jdConfig.Merchant, jdConfig.Terminal, chinaBank.Data,
                jdConfig.Md5Key);
            if (exceptedSign != chinaBank.Sign)
            {
                throw new BizException("返回结果签名不正确");
            }

            var desDecrypted = JdHelper.DesDecrypt(chinaBank.Data, jdConfig.DesKey);
            return JdHelper.ParseXml<FastPayRsp>(desDecrypted);
        }

        public void VerifyDefrayPayCallback(DefrayPayCallbackQM qm)
        {
            qm.NotNull("请求参数");
            qm.sign_data.NotNull("签名");
            qm.sign_type.NotNull("签名类型");
            var pairs = new List<string>();

            qm.GetType().GetProperties().OrderBy(w => w.Name).ToList().ForEach(prop =>
            {
                if (!JdHelper.IGNORES.Contains(prop.Name))
                {
                    var val = Convert.ToString(prop.GetValue(qm));
                    if (!String.IsNullOrEmpty(val))
                    {
                        pairs.Add($"{prop.Name}={val}");
                    }
                }
            });
            var signSrc = String.Join("&", pairs);
            var sign = JdHelper.ComputeSha1(signSrc + jdConfig.Sha256SignKey);
            if (sign!= qm.sign_data)
            {
                throw new BizException("签名不正确");
            }
        }
    }
}
