using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class SssModel : _BasePageModel
    {
        public GenericValueDTO data = new GenericValueDTO();
        public SssModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            data = _genericValueService.Get(211).Data;
        }
    }
}
