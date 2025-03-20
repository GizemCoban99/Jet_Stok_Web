namespace EntityLayer.DTOs
{
    public class MarketplaceCommissionRateDTO
    {
        public int id { get; set; }
        public int marketplace_id { get; set; }
        public int category_id { get; set; }
        public decimal commision_rate { get; set; }
    }
}
