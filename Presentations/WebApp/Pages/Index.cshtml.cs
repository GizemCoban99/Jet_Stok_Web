using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class IndexModel : _BasePageModel
    {
        public GenericValueDTO data = new GenericValueDTO();
        public static List<GenericValueDTO> list = new List<GenericValueDTO>();
        //public List<GenericValueDTO> slider = new List<GenericValueDTO>();
        //public List<GenericValueDTO> logos = new List<GenericValueDTO>();
        public List<GenericValueDTO> features = new List<GenericValueDTO>();
        //public List<GenericValueDTO> packages = new List<GenericValueDTO>();
        public List<GenericValueDTO> comment = new List<GenericValueDTO>();
        public GenericValueDTO currentPage = new GenericValueDTO();

        public List<DynamicValueDTO> dynamicValues = new List<DynamicValueDTO>();
        public List<FAQStructuredDataDTO> fAQDynamicStructuredList = new List<FAQStructuredDataDTO>();

        public GenericValueDTO campaignPopup = new GenericValueDTO();
        public bool isCampaignPopup = false; 
 public List<GenericValueDTO> integrationsList = new List<GenericValueDTO>();

        public IndexModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {

        }

        public void OnGet()
        {
            if (list.Count == 0)
                list = _genericValueService.GetHome(new List<int> { 23,198,20,31}, new List<int> { 1340, 198 }).Data;

            data = list.First(f=>f.id== 1340);

            foreach (var item in list)
            {

                var name = currentPage.name;
                //if (item.type_id == 2)
                //{
                //    slider.Add(item);
                //}
                //else if (item.type_id == 18)
                //{
                //    logos.Add(item);
                //}
                if (item.type_id == 23)
                {
                    features.Add(item);
                }
                else if (item.id == 198)
                {
                    currentPage = item;
                }
                //else if (item.type_id == 6)
                //{
                //    packages.Add(item);
                //}
                else if (item.type_id == 20)
                {
                    comment.Add(item);
                }
                else if (item.type_id == 31)
                {
                    campaignPopup = item;
                }
            }


            for (int i = 1; i <= 5; i++)
            {
                if (i == 1 && !string.IsNullOrEmpty(data.dynamic_title_1))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_1, Description = data.dynamic_description_1 });
                else if (i == 2 && !string.IsNullOrEmpty(data.dynamic_title_2))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_2, Description = data.dynamic_description_2 });
                else if (i == 3 && !string.IsNullOrEmpty(data.dynamic_title_3))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_3, Description = data.dynamic_description_3 });
                else if (i == 4 && !string.IsNullOrEmpty(data.dynamic_title_4))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_4, Description = data.dynamic_description_4 });
                else if (i == 5 && !string.IsNullOrEmpty(data.dynamic_title_5))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_5, Description = data.dynamic_description_5 });
            }
            foreach (var value in dynamicValues)
            {

                fAQDynamicStructuredList.Add(new FAQStructuredDataDTO
                {
                    Type = "Question",
                    Name = value.Title,
                    AcceptedAnswer = new AcceptedAnswerDTO
                    {
                        Type = "Answer",
                        Text = value.Description
                    }
                });
            }

            FAQGoogle = JsonConvert.SerializeObject(fAQDynamicStructuredList);


            if (String.IsNullOrEmpty(HttpContext.Session.GetString("PopIDsEq4")))
            {
                isCampaignPopup = true;
                HttpContext.Session.SetString("PopIDsEq4", "ok");
            } 

            integrationsList = intergrations;

        }

        public JsonResult OnPostInfo(InfoFormDTO info)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("IqIDsEq4")))
            {
                if (string.IsNullOrEmpty(info.Name) || info.Name.Length < 2 || string.IsNullOrEmpty(info.Phone) || info.Phone.Trim().Length < 12 || info.Phone.Trim().Length > 15)
                {
                    return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Bilgiler Hatalı Kontrol Ediniz."));
                }
                else
                {
                    var contactForm = new ContactFormDTO();
                    contactForm.Title = "Jet Stok Bilgi Formu - " + info.Source;
                    contactForm.Name = info.Name.Trim();
                    contactForm.Phone = info.Phone.Trim();
                    contactForm.Description = "ref : " + HttpContext.Session.GetString("referer");
                    var mailResponse = MailSender.MailSend(contactForm);
                    if (mailResponse)
                    {
                        HttpContext.Session.SetString("IqIDsEq4", "ok");
                        return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: true, successMessage: $"{contactForm.Type} Mesajınız Başarılı Bir Şekilde Gönderildi"));
                    }

                }
            }
            return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Mesaj Gönderilirlen Hata Oluştu."));
        }
       
        
        public JsonResult OnPost(ContactFormDTO contactForm)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("AqIDsEq4")))
            {
                var validate = new ContactFormDTOValidator2().Validate(contactForm);
                if (!validate.IsValid)
                {
                    return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: validate.Errors[0].ToString()));
                }
                else
                {
                    contactForm.Title = "Bilgi Talebi ";
                    contactForm.Description = contactForm.Description + " - ref : " + HttpContext.Session.GetString("referer");
                    var mailResponse = MailSender.MailSend(contactForm);
                    if (mailResponse)
                    {
                        HttpContext.Session.SetString("AqIDsEq4", "ok");
                        return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: true, successMessage: $"{contactForm.Type} Mesajınız Başarılı Bir Şekilde Gönderildi"));
                    }

                }
            }
            return new JsonResult(new FormPostResultDTO<ContactFormDTO>(isSuccess: false, successMessage: "Mesaj Gönderilirlen Hata Oluştu."));
        }

        public JsonResult OnPostCookie(bool cookie)
        {
            if (Request.Cookies["DA23sdA"] == "ok")
            {
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

        public JsonResult OnPostCookieOk(bool cookie)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(3);
            cookieOptions.Path = "/";
            Response.Cookies.Append("DA23sdA", "ok", cookieOptions);

            return new JsonResult(true);
        }
    }
}