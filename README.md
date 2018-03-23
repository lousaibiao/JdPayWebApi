# JdPayWebApi
京东支付webapi

## Jd支付
### 包含接口
+ 代付
+ 代付结果查询
+ 快捷签约
+ 快捷支付
+ 代付回调

#### 一些说明
- 代付接口的token验证
```csharp
public void OnActionExecuting(ActionExecutingContext context)
{
    //代付的请求做验证
    if (context.HttpContext.Request.Path.Value.ToLower()
            .IndexOf("api/jd/defraypay", StringComparison.CurrentCulture) == -1) return;
    var contains = context.HttpContext.Request.Headers.TryGetValue(TokenKey, out StringValues token);
    if (!contains || token != Token)
    {
        context.Result = new ContentResult()
        {
            StatusCode = 200,
            ContentType = "application/json",
            Content = JsonConvert.SerializeObject(new
            {
                response_code = 999401,
                response_message = "京东支付：非法请求！"
            })
        };
    }
}
```
如果是代付的地址（api/jd/defraypay），就要验证http请求上做一个验证。Postman截图如下：
![image](https://github.com/lousaibiao/JdPayWebApi/raw/master/%E4%BB%A3%E4%BB%98token.png)



#### 介绍
1. 项目基于ASPNETCORE 2.0
- 参照官方的java版本的demo
- 算签名的时候用了 [BouncyCastle](https://github.com/onovotny/BouncyCastle-PCL),神器。
- 解析xml用了 [YAXLib](https://github.com/sinairv/YAXLib)

2. 关于配置文件

XXX都可以找jd要
```json
  "JdConfig": {
    "Version": "1.0.0",
    "Merchant": "XXX",//商户号
    "Terminal": "00000001",//终端号
    "DesKey": "XXX",
    "Sha256SignKey": "XXX",//这个也是sha1签名时候的key
    "Md5Key": "XXX",
    "FastPayUrl": "https://quick.chinabank.com.cn/express.htm",
    "DefrayPayUrl": "https://mapi.jdpay.com/npp10/defray_pay",
    "TradeQueryUrl": "https://mapi.jdpay.com/npp10/trade_query",
    "DefrayPayNotifyUrl": "http://serverurl/api/jdcallback/defraypay",//自己的地址
    "ExpressPayNotifyUrl": "http://serverurl/api/jdcallback/expresspay",//自己的地址
    "CustomerNo": "XXX",
    "JdPayAuthToken": "这个是配置的代付的token"
  },
```

3. 主要的方法在Site.Common 下面的 JdHelper.cs内
```csharp
/// <summary>
/// pfx文件密码
/// </summary>
private const string pfxPwd = "XXX";
/// <summary>
/// pfx证书，主要是拿私钥
/// </summary>
public static string PfxPath => Path.Combine(AppContext.BaseDirectory, "rsa", "xxx.pfx");
/// <summary>
/// cer证书，拿公钥
/// </summary>
public static string CertPath => Path.Combine(AppContext.BaseDirectory, "rsa","xxx.cer");
```

4. 其他

test 项目就不放上来，太多敏感信息。下面demo里面的一些敏感参数也改成了随机数.

没有引用官方提供的JDAKS.DLL 和 JDSecurity。项目可以直接部署到linux上。

公司项目限定了只用借记卡，所以，这里默认。
```csharp
/// <summary>
/// 收款卡种 支付工具是TRAN时必填
/// 卡类型 借记卡=DE；信用卡=CR
/// </summary>
/// <returns></returns>
[JsonProperty("payee_card_type")]
public string PayeeCardType => "DE";
```

### 代付
- 地址 post /api/jd/defraypay
- 参数
```json
{
    "payee_bank_code": "CMB",
    "payee_account_type": "C",
    "category_code": "HZS",
    "payee_account_no": "XXXX",
    "payee_account_name": "某某公司",
    "out_trade_no": "外部单号",
    "trade_amount": "1",
    "payee_bank_fullname": "某某支行",
    "trade_subject": "20180222100318022910",
    "payee_card_type": "DE",
    "payee_mobile": "15505888888",
    "payee_bank_associated_code": "联行号"
}
```
- 返回结果
```json
{
    "customer_no": "客户号",
    "out_trade_no": "20180323160300",
    "response_code": "0000",
    "response_datetime": "20180323T160343",
    "response_message": "成功",
    "sign_data": null,
    "sign_type": null,
    "trade_amount": "1",
    "trade_currency": "CNY",
    "trade_no": "2018032320000000000015111",
    "trade_status": "WPAR"
}
```

### 代付结果查询
- 地址 get /api/jd/tradequery
- 参数 OutTradeNo=JGCHK00043820180323162213
- 返回
```json
{
    "out_trade_no": "JGCHK00043820180323162213",
    "biz_trade_no": "xxxx",
    "out_trade_date": "Fri Mar 23 15:08:00 CST 2018",
    "trade_class": "DEFY",
    "trade_subject": "code:20180222100318022910",
    "trade_no": "2018032320004200012216605759",
    "seller_info": "{\"customer_type\":\"CUSTOMER_NO\"}",
    "buyer_info": "{\"customer_code\":\"002314032489\",\"customer_type\":\"CUSTOMER_NO\"}",
    "trade_amount": "1",
    "trade_currency": "CNY",
    "refund_amount": null,
    "category_code": "20jd222",
    "confirm_amount": null,
    "trade_respcode": "ACC3030007",
    "trade_respmsg": "账务申请失败:360080004002986837000801:可用余额不足",
    "trade_status": "CLOS",
    "trade_pay_date": "Fri Mar 23 15:08:01 CST 2018",
    "trade_pay_time": null,
    "pay_tool": "TRAN",
    "bank_code": "CMB",
    "card_type": "DE",
    "trade_finish_date": "Fri Mar 23 15:08:01 CST 2018",
    "trade_finish_time": null,
    "return_params": "sfdsf",
    "customer_no": "00000dsf948294",
    "response_datetime": "20180323T171253",
    "sign_type": "SHA-256",
    "sign_data": "153485B335F5B14574A0E205F0C69E6F96E232D8185D86C1EB51C08F93C13875",
    "response_code": "0000",
    "response_message": "成功"
}
```

### 快捷签约
- 地址 post api/jd/ExpressContract
- 签约之后发短信给用户
- 参数 
```json
{
  "card": {
    "bank": "CCB",
    "no": "666345343254325435",
    "name": "哈喽",
    "idNo": "330998294289348729",
    "phone": "15887670441"
  },
  "trade": {
    "id": "20180321192600",
    "amount": "1"
  }
}
```
- 返回
```json
{
    "trade": {
        "type": "V",
        "id": null,
        "amount": null,
        "currency": null
    },
    "return": {
        "code": "EEN0015",
        "desc": "交易已成功，请勿重复支付"
    }
}
```

### 快捷支付
- 地址 post api/jd/fastpay
- 传收到的验证码
- 参数
```json
{
  "card": {
    "bank": "CCB",
    "no": "6213201540002345779",
    "name": "哈喽",
    "idNo": "330124199908030789",
    "phone": "1507655441"
  },
  "trade": {
    "id": "20180321192600",
    "amount": "1",
    "code": "762186",
    "notice": "http://www.baidu.com"
  }
}
```
- 返回
```json
{
    "trade": {
        "type": "S",
        "id": "20180321192600",
        "amount": "1",
        "currency": "CNY",
        "date": "20180321",
        "time": "204610",
        "note": null,
        "status": "0"
    },
    "return": {
        "code": "EES0035",
        "desc": "短信验证码已过期，请重新获取"
    }
}
```
### 代付回调
```json
/// <summary>
/// 代付回调地址
/// </summary>
/// <param name="resp"></param>
/// <returns></returns>
[HttpPost,Route("defraypay")]
public async Task<IActionResult> DefrayPay(DefrayPayCallbackQM qm)
{
    logger.Info($"代付回调返回:{JsonConvert.SerializeObject(qm)}");
    jdService.VerifyDefrayPayCallback(qm);
    //todo 处理代付逻辑
    return Content("ok","text/plain");
}
/// <summary>
/// 快捷回调
/// </summary>
/// <param name="resp"></param>
/// <returns></returns>
[HttpPost,Route("expresspay")]
public async Task<IActionResult> ExpressPay(string resp)
{
    logger.Info($"快捷回调:{resp}");
    await jdService.ParseFastPayAsync(resp);
    //todo 处理快捷逻辑
    return Content("ok","text/plain");
}
```
