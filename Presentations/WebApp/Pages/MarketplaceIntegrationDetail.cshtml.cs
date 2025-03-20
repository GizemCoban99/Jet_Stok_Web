using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Newtonsoft.Json;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class MarketplaceIntegrationDetailModel : _BasePageModel
    {
        public PackageDTO packages = new PackageDTO();

        public GenericValueDTO data = new GenericValueDTO();
        public GenericValueDTO dataBase = new GenericValueDTO();
        public List<GenericValueDTO> integrations = new List<GenericValueDTO>();
        public List<DynamicValueDTO> dynamicValues = new List<DynamicValueDTO>();
        public DynamicValueDTO dynamicValue = new DynamicValueDTO();
        public List<FAQStructuredDataDTO> fAQDynamicStructuredList = new List<FAQStructuredDataDTO>();



        public MarketplaceIntegrationDetailModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }


        public void OnGet(string base_url, string url)
        {

            if (url == "epttavm-2023-yili-komisyon-oranlari")
            {
                url = "ptt-avm-komisyon-oranlari-2024";
                Response.Redirect("/blog/"+ url, true);
            }
            else if (url == "trendyol-2023-yili-komisyon-oranlari")
            {
                url = "trendyol-komisyon-oranlari-2024";
                Response.Redirect("/blog/"+ url, true);
            }
            else if (url == "2023-de-stratejinize-yon-verecek-10-pazarlama-kitabi")
            {
                url = "2024-de-stratejinize-yon-verecek-10-pazarlama-kitabi";
                Response.Redirect("/blog/"+ url, true);
            }
            else if (url == "2023-ptt-kargo-ucreti")
            {
                url = "ptt-kargo-ucreti-2024";
                Response.Redirect("/blog/"+ url, true);
            }
            else if (url == "ciceksepeti-2023-yili-komisyon-oranlari")
            {
                url = "ciceksepeti-komisyon-oranlari-2024";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "hepsiburada-2023-yili-komisyon-oranlari")
            {
                url = "hepsiburada-komisyon-oranlari-2024";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "pazarama-2023-yili%20komisyon-oranlari")
            {
                url = "pazarama-komisyon-oranlari-2024";
                Response.Redirect("/blog/" + url, true);
            }


            data = _genericValueService.Get(url).Data;

            if (data.type_id == 15)
            {
                Response.Redirect("/blog/" + url, true);
            }
            else if (data.type_id == 8)
            {
                Response.Redirect("/xml-entegrasyon/" + url, true);
            }
            integrations = _genericValueService.GetListParent(0, 1000, "", data.parent_1_id).Data.Take(3).ToList();

            packages.packages = _genericValueService.GetList(0, 10000, "", 6).Data;

            packages.packageFeature = _genericValueService.GetList(0, 10000, "", 7).Data;

            for (int i = 1; i <= 5; i++)
            {
                if (i == 1 && !string.IsNullOrEmpty(data.dynamic_title_1))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_1, Description = data.dynamic_description_1 });
                else if (i == 2 && !string.IsNullOrEmpty(data.dynamic_title_2))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_2, Description = data.dynamic_description_2 });
                else if (i == 3 && !string.IsNullOrEmpty(data.dynamic_title_3))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_3, Description = data.dynamic_description_3 });
                else if (i == 4 && !string.IsNullOrEmpty(data.dynamic_title_4))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_4, Description = data.dynamic_description_4 });
                else if (i == 5 && !string.IsNullOrEmpty(data.dynamic_title_5))
                    dynamicValues.Add(new DynamicValueDTO { Title = data.dynamic_title_5, Description = data.dynamic_description_5 });
            }
            foreach (var value in dynamicValues)
            {

                fAQDynamicStructuredList.Add(new FAQStructuredDataDTO
                {
                    Type = "Question",
                    Name = value.Title,
                    AcceptedAnswer = new AcceptedAnswerDTO
                    {
                        Type = "Answer",
                        Text = value.Description
                    }
                });
            }

            FAQGoogle = JsonConvert.SerializeObject(fAQDynamicStructuredList);

        }
    }
}
