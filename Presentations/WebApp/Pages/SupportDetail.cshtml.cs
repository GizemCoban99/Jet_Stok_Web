using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class SupportDetailModel : _BasePageModel
    {
        public GenericValueDTO supportsDetail = new GenericValueDTO();
        public SupportDetailModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet(string url)
        {
            supportsDetail = _genericValueService.Get(url).Data;
        }
    }
}
