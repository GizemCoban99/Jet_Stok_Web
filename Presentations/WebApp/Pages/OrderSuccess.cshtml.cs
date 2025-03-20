using AutoMapper;
using BusinessLayer.Services;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class OrderSuccessModel : _BasePageModel
    {
        public string order_id { get; set; }
        public OrderSuccessModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet(string orderId)
        {
            order_id = orderId;
        }
    }
}
