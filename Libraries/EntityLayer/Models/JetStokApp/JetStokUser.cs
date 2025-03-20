namespace EntityLayer.Models.JetStokApp
{
    public class JetStokUser
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? last_login_date { get; set; }
        public string password_second { get; set; }
        public string password { get; set; }
        public long company_id { get; set; }
        public string company_name { get; set; }
        public string phone { get; set; }
        public int role_id { get; set; }
        public string role_name { get; set; }
        public string sms_code { get; set; }
        public string reset_code { get; set; }
        public string tc_number { get; set; }
        public DateTime sms_code_date { get; set; }
        public bool login_user { get; set; }
        public string admin_login_key { get; set; }
    }
}
