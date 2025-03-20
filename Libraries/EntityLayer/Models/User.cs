using Microsoft.AspNetCore.Http;

namespace EntityLayer.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? last_login_date { get; set; }
        public string password_second { get; set; }
        public string password { get; set; }  
        public string phone { get; set; }
        public int role_id { get; set; }
        public string role_name { get; set; } 
        public string tc_number { get; set; }
        public bool login_user { get; set; } 
        public string image { get; set; }
        public IFormFile profile_image { get; set; }   
         
    }
}
