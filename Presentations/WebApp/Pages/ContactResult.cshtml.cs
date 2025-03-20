using AutoMapper;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class ContactResultModel : _BasePageModel
    {
        public ContactResultModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
        }
    }
}
