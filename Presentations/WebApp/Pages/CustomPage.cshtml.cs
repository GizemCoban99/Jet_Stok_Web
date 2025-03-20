using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class CustomPageModel : _BasePageModel
    {
        public GenericValueDTO data = new GenericValueDTO();
        public CustomPageModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet(string page)
        {
            data = _genericValueService.Get(HttpContext.Request.Path.Value.Substring(1)).Data;


        }
    }
}
