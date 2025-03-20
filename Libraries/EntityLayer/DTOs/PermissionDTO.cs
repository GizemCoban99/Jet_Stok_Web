namespace EntityLayer.DTOs
{
    public class PermissionDTO
    {
        public int id { get; set; }
        public string name { get; set; } 
        public int group_id { get; set; }
        public string group_name { get; set; }
        public string page { get; set; }
        public string handler_method_name { get; set; }
    }
}
