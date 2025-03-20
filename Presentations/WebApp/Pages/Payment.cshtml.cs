using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class PaymentModel : _BasePageModel
    {
        public GenericValueDTO data = new GenericValueDTO();

        public PaymentModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            data = _genericValueService.Get(99).Data;
        }
    }
}
