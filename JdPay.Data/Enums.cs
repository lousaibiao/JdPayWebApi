namespace JdPay.Data
{
    public class Enums
    {
        
    }
    /// <summary>
    /// 收款账户类型
    /// </summary>
    public enum PayeeAccountType
    {
        ///对私
        P=1,
        ///对公
        C=2
    }
    /// <summary>
    /// 提交业务渠道
    /// </summary>
    public enum TradeSouce
    {
        /// <summary>
        /// 外部商户PC
        /// </summary>
        OUT_PC=1,
        /// <summary>
        /// 外部商户APP
        /// </summary>
        OUT_APP=2
    }
    /// <summary>
    /// 支付工具
    /// </summary>
    public enum PayTool
    {
        /// <summary>
        /// 代付到余额
        /// </summary>
        ACCT=1,
        /// <summary>
        /// 代付到小金库
        /// </summary>
        XJK=2,
        /// <summary>
        /// 代付到银行卡
        /// </summary>
        TRAN=3
    }

    public enum CardType
    {
        /// <summary>
        /// 信用卡
        /// </summary>
        C=1,
        /// <summary>
        /// 借记卡
        /// </summary>
        D=2
    }
    /// <summary>
    /// 交易类型编码表
    /// </summary>
    public enum TradeType
    {
        /// <summary>
        /// 签约
        /// </summary>
        V=1,
        /// <summary>
        /// 消费
        /// </summary>
        S=2,
        /// <summary>
        /// 查询
        /// </summary>
        Q=3,
        /// <summary>
        /// 退款
        /// </summary>
        R=4
    }
}