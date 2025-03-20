namespace EntityLayer.Models
{
    public class GenericValue
    {
        public int id { get; set; }
        public int type_id { get; set; } 
        public string name { get; set; }
        public string summary { get; set; }
        public string image { get; set; }
        public string image_description { get; set; }
        public string image_banner { get; set; }
        public string seo_url { get; set; }
        public string seo_title { get; set; }
        public string seo_description { get; set; }
        public string content_1 { get; set; }
        public string content_2 { get; set; }
        public string content_3 { get; set; }
        public string content_4 { get; set; }
        public string content_5 { get; set; }
        public string content_6 { get; set; } 
        public string content_7 { get; set; } 
        public int parent_1_id { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public int order_number { get; set; }
        //public bool is_sss { get; set; }
        public string dynamic_title_1 { get; set; }
        public string dynamic_description_1 { get; set; }
        public string dynamic_title_2 { get; set; }
        public string dynamic_description_2 { get; set; }
        public string dynamic_title_3 { get; set; }
        public string dynamic_description_3 { get; set; }
        public string dynamic_title_4 { get; set; }
        public string dynamic_description_4 { get; set; }
        public string dynamic_title_5 { get; set; }
        public string dynamic_description_5 { get; set; }
        public int jetstok_package_id { get; set; }
        public bool is_pricing_summary { get; set; }

    }
}
