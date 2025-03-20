using AutoMapper;
using BusinessLayer.Services;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class CareerModel : _BasePageModel
    {
        public CareerModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
        }
    }



}
