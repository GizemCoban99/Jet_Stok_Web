using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class SupportModel : _BasePageModel
    {
        public List<GenericValueDTO> supports = new List<GenericValueDTO>();
        public List<GenericValueDTO> subCategory = new List<GenericValueDTO>();
        public SupportModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            supports = _genericValueService.GetList(0, 1000, "", 21).Data;
            subCategory = _genericValueService.GetList(0, 1000, "", 22).Data;
        }
    }
}
