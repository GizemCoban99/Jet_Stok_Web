namespace EntityLayer.Models.JetStokApp
{
    public class CompanyPackage : PackageBase
    {
        public int package_id { get; set; }
        public long company_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public decimal sale_price { get; set; }
        public bool is_sale { get; set; }
    }
}
