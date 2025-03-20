using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Newtonsoft.Json;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class PricingModel : _BasePageModel
    {
        public GenericValueDTO data = new GenericValueDTO();

        public List<DynamicValueDTO> dynamicValues = new List<DynamicValueDTO>();
        public List<FAQStructuredDataDTO> fAQDynamicStructuredList = new List<FAQStructuredDataDTO>();


        public PackageDTO packages = new PackageDTO();
        public PricingModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            data = _genericValueService.Get(1341).Data;
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
