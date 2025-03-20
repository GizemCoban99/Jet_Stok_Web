using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using EntityLayer.Models;
using EntityLayer.Models.IPara;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class BuyPackageModel : _BasePageModel
    {
        private static JetStokAppService _jetStokAppService;
        public string PaymentError = "";
        public PaymentForm formModel;
        public BuyPackageModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
            _jetStokAppService = new JetStokAppService(mapper);
        }

        public void OnGet(string id, string error = "")
        {
            pageTitle = "Ödeme Formu";
            if (string.IsNullOrEmpty(id))
                Response.Redirect("/");

            var userData = _jetStokAppService.GetUserByGuid(id);
            if (userData.Data == null)
                Response.Redirect("/");

            formModel = new PaymentForm() { guid = id };
            
            if (!string.IsNullOrEmpty(error))
                PaymentError = error;
        }
        public JsonResult OnPostStartCheckout(PaymentForm formModel)
        {

            var customer = _jetStokAppService.GetUserByGuid(formModel.guid);
            if (customer.Data == null)
                return new JsonResult(new ServiceResultDTO<object>(false, messages: new List<string> { "Müşteri bilgisi çekilirken hata oluştu." }));

            var userPackage = _jetStokAppService.GetActivePackage(customer.Data.company_id);
            if (userPackage.Data == null)
                return new JsonResult(new ServiceResultDTO<object>(false, messages: new List<string> { "Paket bilgisi çekilirken hata oluştu." }));

            var package = _genericValueService.GetPackagesByJetstokId(userPackage.Data.package_id);
            if (package.Data == null)
                return new JsonResult(new ServiceResultDTO<object>(false, messages: new List<string> { "Paket Hatası" }));

            IPara3DRequestModel request = new();

            var validate = new PaymentFormValidator().Validate(formModel);

            if (!validate.IsValid)
                return new JsonResult(new ServiceResultDTO<object>(false, messages: validate.Errors.Select(q => q.ErrorMessage).ToList()));

            var date = formModel.ExpirationDate.Split("/");
            if (date.Length <= 1)
                return new JsonResult(new ServiceResultDTO<object>(false, messages: new List<string> { "Tarih hatası." }));
            var settings = new IParaSettings();

            request.Version = settings.Version;
            request.Amount = (Convert.ToInt32(package.Data.summary) * 100).ToString();
            request.CardOwnerName = formModel.Name;
            request.CardNumber = formModel.CardNumber;
            request.CardExpireMonth = date[0];
            request.CardExpireYear = date[1];
            request.Cvc = formModel.SecurityCode;

            request.UserId = string.Empty;
            request.CardId = string.Empty;
            request.Mode = settings.Mode;
            request.Language = "tr-TR";
            request.SuccessUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/ipara";
            request.FailUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/ipara";

            request.Purchaser = new();
            request.Purchaser.Name = customer.Data.name;
            request.Purchaser.SurName = customer.Data.surname;
            request.Purchaser.Email = customer.Data.email;
            request.Purchaser.ClientIp = HttpContext.Connection.LocalIpAddress.ToString();

            request.Products = new List<IParaProduct>();
            IParaProduct p = new();
            p.Title = package.Data.name;
            p.Code = "PKTW000" + package.Data.id;
            p.Price = (Convert.ToInt32(package.Data.summary) * 100).ToString();
            p.Quantity = 1;
            request.Products.Add(p);

            //callbak match için gönderilen parametre
            request.Echo = formModel.guid.ToString();

            string threeDform = IPara3DRequestModel.Execute(request, settings);

            return new JsonResult(new ServiceResultDTO<object>(true, data: threeDform));
        }
    }
}
