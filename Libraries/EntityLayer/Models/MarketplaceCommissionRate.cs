namespace EntityLayer.Models
{
    public class MarketplaceCommissionRate
    {
        public int id { get; set; }
        public int marketplace_id { get; set; }
        public int category_id { get; set; }
        public decimal commision_rate { get; set; }
    }
}
