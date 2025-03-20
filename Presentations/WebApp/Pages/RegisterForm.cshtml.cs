using AutoMapper;
using BusinessLayer.Services;
using CoreLayer;
using EntityLayer.DTOs;
using EntityLayer.DTOs.JetStokApp;
using EntityLayer.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class RegisterFormModel : _BasePageModel
    {
        public List<GenericValueDTO> list = new List<GenericValueDTO>();
        public RegisterFormDTO demoForm = new RegisterFormDTO();
        private static JetStokAppService _jetStokAppService;
        public bool SmsAuth { get; set; }
        public RegisterFormModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
            _jetStokAppService = new JetStokAppService(mapper);
        }
        public void OnGet(int id, string verify = "", string phone = "")
        {
            SmsAuth = !string.IsNullOrEmpty(verify);
            demoForm.PackageId = id;
            demoForm.Phone = phone;
        }

        public JsonResult OnPost(RegisterFormDTO demoForm, bool smsAuth, string smsCode)
        {
            if (!smsAuth)
            {
                this.demoForm = demoForm;
                var validate = new RegisterFormDTOValidator().Validate(demoForm);
                if (!validate.IsValid)
                    return new JsonResult(new FormPostResultDTO<RegisterFormDTO>(isSuccess: false, successMessage: validate.Errors.FirstOrDefault().ErrorMessage));
                
                if (_jetStokAppService.GetUserByEmail(demoForm.Email).Data != null)
                    return new JsonResult(new FormPostResultDTO<RegisterFormDTO>(isSuccess: false, successMessage: "Girilen email sistemde kay�tl�d�r. L�tfen sat�n al�m i�in ekibimizle ileti�ime ge�iniz."));

                var smsCodeGenerate = Globals.GenerateSmsCode();
                demoForm.Source = "Pazaryeri";
                var smsResult = SmsSender.Send(new Message[] { new Message { dest = demoForm.Phone, msg = "Jet Stok i�in do�rulama kodunuz: " + smsCodeGenerate } });
                if (smsResult)
                {
                    HttpContext.Session.SetString("User-" + demoForm.Phone, JsonConvert.SerializeObject(demoForm));
                    HttpContext.Session.SetString("User-Verify-" + demoForm.Phone, smsCodeGenerate);
                    return new JsonResult(new FormPostResultDTO<RegisterFormDTO>(isSuccess: true, redirectUrl: "/kayit-form/" + demoForm.PackageId + "?verify=1&phone=" + demoForm.Phone));
                }
            }
            else
            {
                if (HttpContext.Session.GetString("User-Verify-" + demoForm.Phone) != null && HttpContext.Session.GetString("User-Verify-" + demoForm.Phone) == smsCode)
                {
                    var userDataSession = HttpContext.Session.GetString("User-" + demoForm.Phone);
                    if (userDataSession == null)
                        return new JsonResult(new FormPostResultDTO<RegisterFormDTO>(isSuccess: false, successMessage: "Hata olu�tu tekrar deneyiniz.", redirectUrl: "/kayit-form/" + demoForm.PackageId));

                    var userData = JsonConvert.DeserializeObject<RegisterFormDTO>(userDataSession);
                    var modelCompany = _mapper.Map<CompanyDTO>(userData);
                    modelCompany.id = 0;
                    modelCompany.start_date = DateTime.Now;
                    modelCompany.end_date = DateTime.Now.AddYears(1);
                    var dataPackage = _genericValueService.Get((int)userData.PackageId);
                    if (dataPackage.Data == null)
                        return new JsonResult(new FormPostResultDTO<RegisterFormDTO>(isSuccess: false, successMessage: "Paket i�leminde hata olu�tu."));

                    var addCompany = _jetStokAppService.AddJetstokCompany(modelCompany);
                    if (addCompany.isSuccess)
                    {
                        _jetStokAppService.AddJetstokPackage(dataPackage.Data.jetstok_package_id, addCompany.data.id, dataPackage.Data.summary);

                        var modelUser = _mapper.Map<JetStokUserDTO>(userData);
                        modelUser.id = 0;
                        modelUser.role_id = 1;
                        modelUser.company_id = addCompany.data.id;
                        modelUser.admin_login_key = Guid.NewGuid().ToString();
                        var addUser = _jetStokAppService.JetstokUserAdd(modelUser);
                        return new JsonResult(new FormPostResultDTO<RegisterFormDTO>(isSuccess: true, redirectUrl: "/satin-al/" + modelUser.admin_login_key));
                    }
                }

                return new JsonResult(new FormPostResultDTO<RegisterFormDTO>(isSuccess: false, successMessage: "Do�rulama kodu Yanl�� girildi."));
            }
            return new JsonResult(new FormPostResultDTO<RegisterFormDTO>(isSuccess: false, successMessage: "Mesajı Gönderilirken Hata Oluştu."));
        }
    }
}
