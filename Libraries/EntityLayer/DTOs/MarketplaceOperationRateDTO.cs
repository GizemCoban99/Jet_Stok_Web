namespace EntityLayer.DTOs
{
    public class MarketplaceOperationRateDTO
    {
        public int id { get; set; }
        public int marketplace_id { get; set; }
        public decimal operation_price { get; set; }
        public decimal listing_price { get; set; }
    }
}
