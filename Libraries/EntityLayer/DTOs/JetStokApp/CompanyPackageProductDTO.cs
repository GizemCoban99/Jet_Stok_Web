namespace EntityLayer.DTOs.JetStokApp
{
    public class CompanyPackageProductDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal yearly_price { get; set; }
        public decimal sale_price { get; set; }
        public int product_count { get; set; }
        public long package_id { get; set; }
        public int package_product_id { get; set; }
    }
}
