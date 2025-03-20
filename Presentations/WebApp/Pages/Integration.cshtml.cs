using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class IntegrationModel : _BasePageModel
    {
        public List<GenericValueDTO> integrationsList = new List<GenericValueDTO>(); // Ana entegrasyonlar
        public Dictionary<int, List<GenericValueDTO>> subIntegrations = new Dictionary<int, List<GenericValueDTO>>(); // Alt entegrasyonlar

        public IntegrationModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet()
        {
            integrationsList = _genericValueService.GetListParent(0, 1000, "", 0).Data
                                .Where(x => x.type_id == 3)
                                .ToList();

            foreach (var integration in integrationsList)
            {
                int parentId = integration.id;

                var subList = _genericValueService.GetListParent(0, 1000, "", parentId).Data
                                  .ToList();


                if (subList.Any())
                {
                    subIntegrations[parentId] = subList;
                }
            }
        }
    }
}
