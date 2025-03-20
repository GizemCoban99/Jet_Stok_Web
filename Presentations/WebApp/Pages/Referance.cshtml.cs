using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class ReferanceModel : _BasePageModel
    {
        public List<GenericValueDTO> list = new List<GenericValueDTO>();
        public ReferanceModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            list = _genericValueService.GetList(0, 10000, "", 4).Data;
        }
    }
}
