using EntityLayer.DTOs;
using EntityLayer.DTOs.JetStokApp;
using System.Net.Mail;
using System.Text;

namespace BusinessLayer.Services
{
    public static class MailSender
    {
        public static string _MailTemplateUrl()
        {
            var url = $@"C:\inetpub\wwwroot\jetstok\www.jetstok.com\wwwroot\MailTemplate\";

            //info.html sayfasının local adresi
            //var url = @"C:\Users\Pomelosoft\Documents\GitHub\Jet_Stok_Web_Site\Presentations\WebApp\wwwroot\MailTemplate\";
            //var url = @"C:\Users\yasar\Documents\GitHub\Jet_Stok_Web_Site\Presentations\WebApp\wwwroot\MailTemplate\";
            return url;
        }


        public static bool MailSend(ContactFormDTO contactForm)
        {
            bool response = false;
            try
            {
                string Body = string.Format(File.ReadAllText(_MailTemplateUrl() + "info.html"), contactForm.Name, contactForm.Email, contactForm.Phone, contactForm.Title, contactForm.Description);
                response = SendEmail(contactForm.Email, contactForm.Name, contactForm.Title, Body);
                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        public static bool MailSend(DemoFormDTO demoForm)
        {

            bool response = false;
            try
            {
                string Body = string.Format(File.ReadAllText(_MailTemplateUrl() + "info.html"), demoForm.Name, demoForm.Surname, demoForm.Email, demoForm.Phone, demoForm.CompanyName);
                response = SendEmail2(demoForm.Email, demoForm.Name, Body,demoForm.Source);
                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }
        public static bool MailSend(JetStokUserDTO user,string message="")
        {

            bool response = false;
            try
            {
                string Body = string.Format(File.ReadAllText(_MailTemplateUrl() + "ecommerce-info.html"), user.name+" "+ user.surname, user.email, user.phone,"Eticaret Satış", message);
                response = SendEmail2("", "", Body, "E-Ticaret Satış");
                return response;
            }
            catch (Exception)
            {
                return response;
            }
        }

        private static bool SendEmail2(string email, string name, string body,string source)
        {
            try
            {
                MailMessage m = new MailMessage
                {
                    From = new MailAddress("noreply@pomelostok.com", "Jet Stok - İletişim Formu "+ source),
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    Body = body
                };
                m.To.Add("elif.pehlivan@pomelosoft.com");
                m.To.Add("info@jetstok.com");
                m.To.Add("hulya@jetstok.com");
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential("noreply@pomelostok.com", "eiekavmrzbdkrzib");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.yandex.ru";
                smtp.Send(m);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static bool SendEmail(string email, string name, string title, string body)
        {
            try
            {
                MailMessage m = new MailMessage
                {
                    From = new MailAddress("noreply@pomelostok.com", "Jet Stok - İletişim Formu"),
                    Subject = title,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    Body = body
                };
                m.To.Add("elif.pehlivan@pomelosoft.com");
                m.To.Add("hulya@jetstok.com");
                m.To.Add("info@jetstok.com"); 
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential("noreply@pomelostok.com", "eiekavmrzbdkrzib");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.yandex.ru";
                smtp.Send(m);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
    }
}
