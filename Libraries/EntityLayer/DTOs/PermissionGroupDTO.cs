namespace EntityLayer.DTOs
{
    public class PermissionGroupDTO
    {
        public int group_id { get; set; }
        public string group_name { get; set; }
        public List<PermissionDTO> permissions { get; set; } = new List<PermissionDTO>();
    }
}
