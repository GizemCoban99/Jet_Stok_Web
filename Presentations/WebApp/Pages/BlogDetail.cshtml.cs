using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class BlogDetailModel : _BasePageModel
    {
        public GenericValueDTO data = new GenericValueDTO();
        public List<GenericValueDTO> blogListByCategory = new List<GenericValueDTO>();

        public BlogDetailModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet(string url)
        {
            if (url == "turkiyenin-en-buyuk-e-ticaret-siteleri" || url == "e-ticaret-nedir")
            {
                Response.Redirect("/e-ticaret", true);
            }
            else if (url == "trendyolda-nasil-magaza-acilir" || url=="trendyolda-satis-nasil-yapilir")
            {
                url = "trendyol-pazaryerinde-satis-yapmak";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "hepsiburadada-nasil-magaza-acilir")
            {
                url = "hepsiburada-pazaryerinde-nasil-magaza-acilir";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "dropshipping-nedir-stoksuz-e-ticaret-nasil-yapilir")
            {
                url = "dropshipping-nedir";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "2023-de-stratejinize-yon-verecek-10-pazarlama-kitabi")
            {
                url = "2024-de-stratejinize-yon-verecek-10-pazarlama-kitabi";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "trendyol-2023-yili-komisyon-oranlari")
            {
                url = "trendyol-komisyon-oranlari-2024";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "hepsiburada-2023-yili-komisyon-oranlari")
            { 
                url = "hepsiburada-komisyon-oranlari-2024";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "n11-2023-yili-komisyon-oranlari") 
            {
                url = "n11-komisyon-oranlari-2024";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "ciceksepeti-2023-yili-komisyon-oranlari") 
            {
                url = "ciceksepeti-komisyon-oranlari-2024";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "epttavm-2023-yili-komisyon-oranlari") 
            {
                url = "ptt-avm-komisyon-oranlari-2024";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "pazarama-2023-yili komisyon-oranlari") 
            {
                url = "pazarama-komisyon-oranlari-2024";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "2023-ptt-kargo-ucreti") 
            {
                url = "ptt-kargo-ucreti-2024";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "kitle-psikolojisi-nedir-d42ef7") 
            {
                url = "kitle-psikolojisi-nedir";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "ozon-pazaryerinde-nasil-satis-yapılır") 
            {
                url = "ozon-pazaryerinde-nasil-satis-yapilir";
                Response.Redirect("/blog/" + url, true);
            }
            
            else if (url == "amazon-da-satis-yapmanin-avantajlari")
            {
                url = "amazonda-satis-yapmanin-avantajlari";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "yurtdisina-ürün-satisi-nasil-yapilir") 
            {
                url = "yurtdisina-urun-satisi-nasil-yapilir";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "eticaret-sitesi-fiyatlarini-etkileyen-temel-faktörler") 
            {
                url = "eticaret-sitesi-fiyatlarini-etkileyen-temel-faktorler";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "eticaretin-tüketici-ve-isletme-icin-firsatlari") 
            {
                url = "eticaretin-tuketici-ve-isletme-icin-firsatlari";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "shopify-ne-demektir")
            {
                url = "shopify-nedir-ve-nasil-kullanilir";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "online-pazaryeri-entegrasyonu-nedir-")
            {
                url = "online-pazaryeri-entegrasyonu-nedir";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "e-ticarette-hizli-teslimat-icin-ipuclari")
            {
                url = "hizli-teslimat-icin-ipuclari";
                Response.Redirect("/blog/" + url, true);
            } 
            else if (url == "e-ticaret-sitesi-kurmak-icin-guncel-haberler-f620c8")
            {
                url = "e-ticaret-sitesi-kurmak-icin-guncel-haberler-ac8bb8";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "gelir-vergisi-nedir")
            {
                url = "gelir-vergisi-nedir-nasil-hesaplanir-nasil-odenir";
                Response.Redirect("/blog/" + url, true);
            } 
            else if (url == "n11-nedir-pazaryerinde-nasil-magaza-acilir")
            {
                url = "n11-satici-olmak-icin-bilmeniz-gerekenler";
                Response.Redirect("/blog/" + url, true);
            }
            else if (url == "en-basarili-ve-pratik-pazaryeri-entegrasyonlari")
            { 
                Response.Redirect("/", true);
            }


            data = _genericValueService.Get(url).Data;
            blogListByCategory = _genericValueService.GetBlogCategoryList().Data;



             

        }
    }
}
