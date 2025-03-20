using AutoMapper;
using BusinessLayer.Services;
using CoreLayer;
using CoreLayer.Enums;
using EntityLayer.DTOs;
using EntityLayer.Models.IPara;
using EntityLayer.Models.JetStokApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApp.Pages.Shared;

namespace WebApp.Pages.App.IPara
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    [AllowAnonymous]
    public class IParaResultModel : _BasePageModel
    {
        private static JetStokAppService _jetStokAppService;

        public IParaResultModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
            _jetStokAppService = new JetStokAppService(mapper);
        }

        public void OnGet()
        {
            Response.Redirect("/");

        }
        public void OnPost([FromForm] IPara3DResponse response)
        {
            var customerGuid = response.Echo;
            var redirectOrder = true;
            if (response.PublicKey != new IParaSettings().PublicKey || string.IsNullOrEmpty(customerGuid))
                Response.Redirect("/satin-al/" + customerGuid + "?error=" + WebUtility.UrlEncode("Ödeme oluþturulurken hata oluþtu. Lütfen destek talebi açýnýz. OrderId:" + response.OrderId));

            if (response.Result == "1")
            {
                var user = _jetStokAppService.GetUserByGuid(customerGuid);
                var addPaymentModel = new PaymentLog()
                {
                    identity_number = "11111111111",
                    amount = 1,
                    companyid = user.Data.company_id,
                    is_yearly = true,
                    total_price = response.Amount.ConvertToDecimal() / 100,
                    shipmentAddress = "",
                    status = PaymentStatusEnum.Confirmed,
                    conversation_id = response.OrderId
                };
                //send mail for information if ecommerce
                var userPackage = _jetStokAppService.GetActivePackage(user.Data.company_id);
                if (userPackage.Data != null)
                {
                    var package = _genericValueService.GetPackagesByJetstokId(userPackage.Data.package_id);
                    if (package.Data != null && package.Data.type_id == 35)
                    {
                        MailSender.MailSend(user.Data,package.Data.name);
                        redirectOrder = false;
                    }
                }
                var result = _jetStokAppService.InsertPaymentRecord(addPaymentModel);

                if (result.IsSuccess)
                {
                    var approveResult = _jetStokAppService.UpdateCompanyApprovedActive(user.Data.company_id);
                    if (approveResult && redirectOrder)
                        Response.Redirect("/basarili?orderId=" + response.OrderId);
                    else if(approveResult && !redirectOrder)
                        Response.Redirect("/e-ticaret-basarili?orderId=" + response.OrderId);

                }
                else
                {
                    Response.Redirect("/satin-al/" + customerGuid + "?error=" + WebUtility.UrlEncode("Ödeme oluþturulurken hata oluþtu.Lütfen destek talebi açýnýz.OrderId:" + response.OrderId));
                }
            }
            else
            {
                Response.Redirect("/satin-al/" + customerGuid + $"?error=" + WebUtility.UrlEncode(response.ErrorMessage));
            }
        }
    }
}
