using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class ContactModel : _BasePageModel
    {
        public ContactFormDTO contactForm = new ContactFormDTO();
        public ContactModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
        }


        public JsonResult OnPost(ContactFormDTO contactForm, string selectType)
        {

            if (String.IsNullOrEmpty(HttpContext.Session.GetString("AqIDsEq")))
            {
                var typeValue = Convert.ToInt32(selectType);
                if (typeValue == 0)
                {
                    return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Lütfen Şikayet, Öneri veya Bilgi Tiplerinden Birini Seçiniz"));
                }
                else
                {
                    this.contactForm = contactForm;
                    var validate = new ContactFormDTOValidator().Validate(contactForm);
                    if (!validate.IsValid)
                    {
                        return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: validate.Errors[0].ToString()));
                    }
                    else
                    {
                        switch (typeValue)
                        {
                            case 1:
                                contactForm.Type = "Şikayet";
                                break;
                            case 2:
                                contactForm.Type = "Öneri";
                                break;
                            case 3:
                                contactForm.Type = "Bilgi";
                                break;
                        }
                        contactForm.Title = contactForm.Type + " - " + contactForm.Title;

                        contactForm.Description = contactForm.Description + " - ref: " + HttpContext.Session.GetString("referer");

                        var mailResponse = MailSender.MailSend(contactForm);
                        if (mailResponse)
                        {
                             
                            HttpContext.Session.SetString("AqIDsEq", "ok");

                            return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: true, successMessage: $"{contactForm.Type} Mesajınız Başarılı Bir Şekilde Gönderildi"));
                        }


                    }

                }
            }
            return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Mesaj Gönderilirlen Hata Oluştu."));
        }

    }
}
