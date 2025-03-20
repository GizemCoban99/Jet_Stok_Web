using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class MarketplaceIntegrationModel : _BasePageModel
    {

        public GenericValueDTO data = new GenericValueDTO();
        public List<GenericValueDTO> integrationsList = new List<GenericValueDTO>();
        public MarketplaceIntegrationModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet(string url)
        {
            data = _genericValueService.Get(url).Data; 
            integrationsList = _genericValueService.GetListParent(0, 1000, "", data.id).Data;
        }
    }
}
