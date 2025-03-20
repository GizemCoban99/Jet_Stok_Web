using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class DealerModel : _BasePageModel
    {
        public ContactFormDTO contactForm = new ContactFormDTO();
        public List<GenericValueDTO> list = new List<GenericValueDTO>();
        public DealerModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            list = _genericValueService.GetList(0, 10000, "", 8).Data;
        }
        public JsonResult OnPostContact(ContactFormDTO contactForm)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("AqIDsEq2")))
            {
                this.contactForm = contactForm;
                if (string.IsNullOrEmpty(contactForm.Name) || string.IsNullOrEmpty(contactForm.Phone) || string.IsNullOrEmpty(contactForm.Title) || string.IsNullOrEmpty(contactForm.Email))
                    return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Bilgileri kontrol ediniz."));
                else
                {
                    var mailResponse = MailSender.MailSend(contactForm);
                    if (mailResponse)
                    {
                        HttpContext.Session.SetString("AqIDsEq2", "ok");
                        return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: true, successMessage: "Mesajınız Başarılı Bir Şekilde Gönderilmiştir."));
                    }
                }
            }

            return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Mesajı Gönderilirken Hata Oluştu."));
        }
    }
}
