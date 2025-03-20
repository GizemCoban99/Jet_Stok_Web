namespace EntityLayer.DTOs.JetStokApp
{
    public class CompanyPackageDTO : PackageBaseDTO
    {
        public decimal sale_price { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        public long company_id { get; set; }
        public int package_id { get; set; }
        public bool is_sale { get; set; }

        public List<CompanyPackageProductDTO> products { get; set; } = new List<CompanyPackageProductDTO>();
    }
}
