namespace EntityLayer.DTOs
{
    public class AccountInfoDTO
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime create_date { get; set; }
        public DateTime last_login_date { get; set; }
        public string image { get; set; }
        public int role_id { get; set; }
        public bool is_admin_login { get; set; }
    }
}
