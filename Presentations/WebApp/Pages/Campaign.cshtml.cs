using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class CampaignModel : _BasePageModel
    {
        public List<GenericValueDTO> list = new List<GenericValueDTO>();
        public CampaignModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            list = _genericValueService.GetList(0, 10000, "", 5).Data;
        }
    }
}
