using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class ECommerceModel : _BasePageModel
    {
        public ContactFormDTO contactForm = new ContactFormDTO();
        public GenericValueDTO data = new GenericValueDTO();
        public List<GenericValueDTO> list = new List<GenericValueDTO>();
        public List<GenericValueDTO> comment = new List<GenericValueDTO>();
        public ECommerceModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            data = _genericValueService.Get(86).Data;
            list = _genericValueService.GetList(0, 10000, "", 39).Data;

            foreach (var item in list)
            {
                if (item.type_id == 39)
                {
                    comment.Add(item);  
                }
            }


        }

        public JsonResult OnPostContact(ContactFormDTO contactForm)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("AqIDsEq3")))
            {
                this.contactForm = contactForm;
                if (string.IsNullOrEmpty(contactForm.Name) || string.IsNullOrEmpty(contactForm.Phone) || string.IsNullOrEmpty(contactForm.Title) || string.IsNullOrEmpty(contactForm.Email))
                    return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Bilgileri kontrol ediniz."));
                else
                {
                    var mailResponse = MailSender.MailSend(contactForm);
                    if (mailResponse)
                    {
                        HttpContext.Session.SetString("AqIDsEq3", "ok");
                        return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: true, successMessage: "Mesajınız Başarılı Bir şekilde Gönderilmiştir."));
                    }
                }
            }
            return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Mesaj Gönderilirlen Hata Oluştu."));
        }

        public JsonResult OnPost(ContactFormDTO contactForm)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("EqIDsEq4")))
            {
                var validate = new ContactFormDTOValidator2().Validate(contactForm);
                if (!validate.IsValid)
                {
                    return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: validate.Errors[0].ToString()));
                }
                else
                {
                    contactForm.Title = "Bilgi Talebi - E-Ticaret";
                    contactForm.Description = contactForm.Description + " - ref : " + HttpContext.Session.GetString("referer");
                    var mailResponse = MailSender.MailSend(contactForm);
                    if (mailResponse)
                    {
                        HttpContext.Session.SetString("EqIDsEq4", "ok");
                        return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: true, successMessage: $"{contactForm.Type} Mesajınız Başarılı Bir Şekilde Gönderildi"));
                    }

                }
            }
            return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Mesaj Gönderilirlen Hata Oluştu."));
        }
    }
}
