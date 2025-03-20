using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class XmlIntegrationDetailModel : _BasePageModel
    {
        public ContactFormDTO contactForm = new ContactFormDTO();
        public GenericValueDTO data = new GenericValueDTO();
        public PackageDTO packages = new PackageDTO();
        public XmlIntegrationDetailModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet(string url)
        {
            if (string.IsNullOrEmpty(url))
                Response.Redirect("/xml-entegrasyonlar");

            data = _genericValueService.Get(url).Data;

            packages.packages = _genericValueService.GetList(0, 10000, "", 6).Data;

            packages.packageFeature = _genericValueService.GetList(0, 10000, "", 7).Data;

            if (data == null)
                Response.Redirect("/xml-entegrasyonlar");
        }

    }
}
