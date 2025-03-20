using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Policy;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class CampaignDetailModel : _BasePageModel
    {
        public GenericValueDTO data = new GenericValueDTO();
      
		public CampaignDetailModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }
        public void OnGet(string url)
        {

            data = _genericValueService.Get(url).Data;
            
        }
    }
}
