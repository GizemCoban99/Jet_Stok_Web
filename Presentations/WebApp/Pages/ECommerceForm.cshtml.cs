using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class ECommerceForm : _BasePageModel
    {
        public List<GenericValueDTO> list = new List<GenericValueDTO>();
        public DemoFormDTO demoForm = new DemoFormDTO();

        public ECommerceForm(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }
        public void OnGet()
        {
        }

        public JsonResult OnPost(DemoFormDTO demoForm)
        { 
            if (Request.Cookies["TfDZsB"] != "ok")
            {
                this.demoForm = demoForm;
                var validate = new DemoFormDTOValidator().Validate(demoForm);
                if (!validate.IsValid)
                    return new JsonResult(new FormPostResultDTO<DemoFormDTO>(isSuccess: false, successMessage: validate.Errors.FirstOrDefault().ErrorMessage));
                else
                {
                    demoForm.Source="E-Ticaret";
                    demoForm.CompanyName = demoForm.CompanyName + " - ref: " + HttpContext.Session.GetString("referer");
                    var mailResponse = MailSender.MailSend(demoForm);
                    if (mailResponse)
                    {
                        var cookieOptions = new CookieOptions();
                        cookieOptions.Expires = DateTime.Now.AddDays(3);
                        cookieOptions.Path = "/";
                        Response.Cookies.Append("TfDZsB", "ok", cookieOptions);
                        return new JsonResult(new FormPostResultDTO<DemoFormDTO>(isSuccess: true, successMessage: "Mesajınız Başarılı Bir Şekilde Gönderilmiştir."));
                    }
                }
            }
            return new JsonResult(new FormPostResultDTO<DemoFormDTO>(isSuccess: false, successMessage: "Mesajı Gönderilirken Hata Oluştu."));
        }
    }
}
