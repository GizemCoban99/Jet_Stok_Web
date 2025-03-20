using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebApp.Pages.Shared
{
    public abstract class _BasePageModel : PageModel
    {
        protected readonly IMapper _mapper;

        public string pageTitle { get; set; }
        public string pageDescription { get; set; }
        public string pageCanonical { get; set; }

        private static List<GenericValueDTO> blogData = new List<GenericValueDTO>();
        private static List<GenericValueDTO> xmlIntegrationData = new List<GenericValueDTO>();
        private static List<GenericValueDTO> faqData = new List<GenericValueDTO>();
        private static List<GenericValueDTO> intergrationData = new List<GenericValueDTO>();
        public List<FAQStructuredDataDTO> fAQDefaultStructuredList = new List<FAQStructuredDataDTO>();

        public List<GenericValueDTO> blogs = new List<GenericValueDTO>();
        public List<GenericValueDTO> xmlIntegrations = new List<GenericValueDTO>();
        public List<GenericValueDTO> faqs = new List<GenericValueDTO>();
        public List<GenericValueDTO> intergrations = new List<GenericValueDTO>();
        public readonly GenericValueService _genericValueService;
        public string FAQGoogle = string.Empty;




        protected _BasePageModel(IMapper mapper, GenericValueService genericValueService)
        {
            _mapper = mapper;
            _genericValueService = genericValueService;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (blogData.Count == 0)
                blogData = _genericValueService.GetList(0, 10, "", 15).Data;

            blogs = blogData;


            if (faqData.Count == 0)
                faqData = _genericValueService.GetList(0, 1000, "", 13).Data.OrderBy(x => x.order_number).ToList();

            faqs = faqData;

            foreach (var value in faqs)
            {
                fAQDefaultStructuredList.Add(new FAQStructuredDataDTO
                {
                    Type = "Question",
                    Name = value.name,
                    AcceptedAnswer = new AcceptedAnswerDTO
                    {
                        Type = "Answer",
                        Text = value.content_1.Replace("<p>", "").Replace("</p>", "").Replace("<br>", "")
                    }
                });
            }

            faqs.FirstOrDefault().FAQGoogle = JsonConvert.SerializeObject(fAQDefaultStructuredList);



            if (intergrationData.Count == 0)
                intergrationData = _genericValueService.GetList(0, 1000, "", 27).Data.OrderBy(x => x.order_number).ToList();

            intergrations = intergrationData;


            if (xmlIntegrationData.Count == 0)
                xmlIntegrationData = _genericValueService.GetList(0, 1000, "", 8).Data.OrderBy(x => x.order_number).ToList();

            xmlIntegrations = xmlIntegrationData;

            if (!string.IsNullOrEmpty(context.HttpContext.Request.Headers.Referer) && context.HttpContext.Session.GetString("referer")==null)
            {
                context.HttpContext.Session.SetString("referer", context.HttpContext.Request.Headers.Referer);
            }

            if (HttpContext.Session.GetString("is_first") == null && HttpContext.Request.Path.HasValue && HttpContext.Request.Path.ToString().Contains("/blog/"))
            {
                HttpContext.Session.SetString("is_first", "blog");
                ViewData["HasFirstPage"]= "blog";
            }
            else if(HttpContext.Session.GetString("is_first") == null)
            {
                HttpContext.Session.SetString("is_first", ""); 
                ViewData["HasFirstPage"] = "";
            }
            else if (HttpContext.Session.GetString("is_first") == "")
            { 
                ViewData["HasFirstPage"] = "";
            }

            if (context.HttpContext.Session.GetString("referer")!=null)
            {
                ViewData["Referer"] = context.HttpContext.Session.GetString("referer");
            }
        }



    }
}
