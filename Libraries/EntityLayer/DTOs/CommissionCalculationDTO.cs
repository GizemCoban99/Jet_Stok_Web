namespace EntityLayer.DTOs
{
    public class CommissionCalculationDTO
    {
        public decimal sale_price { get; set; }
        public decimal purchase_price { get; set; }
        public int marketplace_id { get; set; }
        public int category_id { get; set; }
        public decimal commision_rate { get; set; }
        public int kdv { get; set; }
    }

    public class CommissionCalculationResponseDTO
    {
        public string price { get; set; }
        public string commission { get; set; }
        public string kdv_price { get; set; }
        public string service_price { get; set; }
        public string percentage_gain { get; set; }
        public bool success { get; set; }
    }
}
