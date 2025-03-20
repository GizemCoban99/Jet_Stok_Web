namespace EntityLayer.DTOs
{
    public class GenericTypeDTO
    {
        public int id { get; set; }
        public string type_name { get; set; }
        public bool is_name { get; set; }
        public bool is_summary { get; set; }
        public bool is_image { get; set; }
        public bool is_description_image { get; set; }
        public bool is_seo_url { get; set; }
        public bool is_seo_title { get; set; }
        public bool is_seo_description { get; set; }
        public bool is_content_1 { get; set; }
        public bool is_content_2 { get; set; }
        public bool is_content_3 { get; set; }
        public bool is_content_4 { get; set; }
        public bool is_content_5 { get; set; }
        public bool is_parent_1 { get; set; }
        public int parent_1_id { get; set; }
        public string parent_1_title { get; set; }
        public bool is_title { get; set; }
        public bool is_subtitle { get; set; }
        public bool is_order_number { get; set; }
        public bool is_sss { get; set; }
    }
}
