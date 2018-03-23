namespace JdPay.Data.CallbackModel
{
    /// <summary>
    /// 代付回调的参数
    /// </summary>
    public class DefrayPayCallbackQM
    {
        public string sign_type { get; set; }
        public string sign_data { get; set; }
        public string trade_no { get; set; }
        public string merchant_no { get; set; }
        public string notify_datetime { get; set; }
        public string bank_code { get; set; }
        public string customer_no { get; set; }
        public string out_trade_no { get; set; }
        public string trade_class { get; set; }
        public string trade_status { get; set; }
        public string pay_tool { get; set; }
        public string trade_pay_time { get; set; }
        public string is_success { get; set; }
        public string card_type { get; set; }
        public string buyer_info { get; set; }
        public string trade_subject { get; set; }
        public string trade_pay_date { get; set; }
        public string trade_finish_time { get; set; }
        public string seller_info { get; set; }
        public string trade_amount { get; set; }
        public string trade_finish_date { get; set; }
        public string refund_amount { get; set; }
        public string category_code { get; set; }
        public string trade_currency { get; set; } 
        
    }
}