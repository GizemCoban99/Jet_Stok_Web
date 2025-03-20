namespace EntityLayer.DTOs
{
    public class PackageDTO
    {
        public List<GenericValueDTO> packages { get; set; }
        public List<GenericValueDTO> packageFeature { get; set; }
        public PackageDTO()
        {
            packages = new List<GenericValueDTO>();
            packageFeature= new List<GenericValueDTO>();
        }
        
    }
}
