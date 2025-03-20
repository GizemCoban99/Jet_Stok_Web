using AutoMapper;
using BusinessLayer.Services;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class MarketPlaceIntegrationDetailRedirectModel : _BasePageModel
    {
        public MarketPlaceIntegrationDetailRedirectModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet(string url)
        {
            Response.Redirect("/" + url, true);
        }
    }
}
