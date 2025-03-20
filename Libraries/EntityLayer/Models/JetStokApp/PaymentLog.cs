using CoreLayer.Enums;

namespace EntityLayer.Models.JetStokApp
{
    public class PaymentLog
    {
        public int id { get; set; }
        public long companyid { get; set; }
        public decimal total_price { get; set; }
        public int installment { get; set; }
        public string shipmentAddress { get; set; }
        public string billAddress { get; set; }
        public string identity_number { get; set; }
        public string tax_no { get; set; }
        public int city { get; set; }
        public int district { get; set; }
        public bool is_yearly { get; set; }
        public double amount { get; set; }
        public DateTime create_date { get; set; }
        public PaymentStatusEnum status { get; set; }
        public int piece { get; set; }
        public string conversation_id { get; set; }
        public string register_code { get; set; }
    }
}
