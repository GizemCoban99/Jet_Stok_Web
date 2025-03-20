namespace EntityLayer.DTOs.Component.Select2
{
    public class Select2Response
    {
        public List<Select2ResponseItem> results { get; set; }

    }
    public class Select2ResponseItem
    {
        public int id { get; set; }
        public string text { get; set; }
    }
}
