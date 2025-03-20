using CoreLayer;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.DTOs.JetStokApp
{
    public class CompanyDTO
    {
        public long id { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int CompanyState { get; set; }
        public string phone { get; set; }
        public string image { get; set; }
        public string tax_number { get; set; }
        public string tax_office { get; set; }
        public int city_id { get; set; }
        public int district_id { get; set; }
        public string address { get; set; }
        public string note { get; set; }
        public IFormFile company_image { get; set; }
        public int is_deleted { get; set; }
        public int approved { get; set; }
        public string invite_code { get; set; }
        public string register_code { get; set; }
        public string comment { get; set; }
        public DateTime call_date { get; set; }
        public string ReferrerUrl { get; set; }
        public string dealer_code { get; set; }
        public long parent_id { get; set; }
        public bool is_tsofis_member { get; set; }
        public bool is_cancel { get; set; }
        public bool is_active { get; set; }
        public bool is_show_new_register { get; set; }
        public bool is_gx { get; set; }
        public string api_key { get; set; }
        public string api_password { get; set; }
        public int api_error_count { get; set; }
        public string supplier_code { get; set; }
        public string app_db_connection { get; set; }
        public bool is_bill_date { get; set; }
        public string app_db_connection_string
        {
            get
            {
                if (string.IsNullOrEmpty(app_db_connection))
                    return "";
                else
                {
                    return Globals.DataEncription(app_db_connection, email);
                }
            }
        }

    }

}
