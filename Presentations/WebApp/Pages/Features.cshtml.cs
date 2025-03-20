using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class FeaturesModel : _BasePageModel
    {
        public List<GenericValueDTO> datas = new List<GenericValueDTO>();
        public FeaturesModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            datas = _genericValueService.GetList(0, 10000, "", 23).Data;
        }
    }
}
