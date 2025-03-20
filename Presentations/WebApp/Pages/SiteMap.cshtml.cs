using AutoMapper;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class SiteMapModel : _BasePageModel
    {
        public SiteMapModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public IActionResult OnGet()
        {
            var responseString = "";

            var datas = _genericValueService.GetAll(0, 100000, "").Data;

            using (var sw = new StringWriter())
            {
                using (var xml = XmlWriter.Create(sw))
                {
                    xml.WriteStartDocument();
                    xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/hakkimizda");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/sikca-sorulan-sorular");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/e-ticaret");
                    xml.WriteEndElement();

                    //xml.WriteStartElement("url");
                    //xml.WriteElementString("loc", "https://www.jetstok.com/bayilerimiz");
                    //xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/bize-ulasin");
                    xml.WriteEndElement();


                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/blogs");
                    xml.WriteEndElement();


                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/kampanyalar");
                    xml.WriteEndElement();


                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/referanslar");
                    xml.WriteEndElement();


                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/fiyatlandirma");
                    xml.WriteEndElement();



                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/e-ticaret-fiyatlari");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/kariyer");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/bayilik-sistemi");
                    xml.WriteEndElement();





                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/entegrasyonlar/pazaryeri-entegrasyonlari");
                    xml.WriteEndElement();


                    foreach (var item in datas.Where(q => q.parent_1_id == 183).ToList())
                    {
                        xml.WriteStartElement("url");
                        xml.WriteElementString("loc", "https://www.jetstok.com/" + item.seo_url);
                        xml.WriteEndElement();
                    }



                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/entegrasyonlar/muhasebe-entegrasyonlari");
                    xml.WriteEndElement();

                    foreach (var item in datas.Where(q => q.parent_1_id == 184).ToList())
                    {
                        xml.WriteStartElement("url");
                        xml.WriteElementString("loc", "https://www.jetstok.com/" + item.seo_url);
                        xml.WriteEndElement();
                    }



                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/entegrasyonlar/web-sitesi-entegrasyonlari");
                    xml.WriteEndElement();

                    foreach (var item in datas.Where(q => q.parent_1_id == 212).ToList())
                    {
                        xml.WriteStartElement("url");
                        xml.WriteElementString("loc", "https://www.jetstok.com/" + item.seo_url);
                        xml.WriteEndElement();
                    }




                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/komisyon-hesaplama");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/n11-komisyon-hesaplama");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/trendyol-komisyon-hesaplama");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/hepsiburada-komisyon-hesaplama");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/ciceksepeti-komisyon-hesaplama");
                    xml.WriteEndElement();

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/pttavm-komisyon-hesaplama");
                    xml.WriteEndElement();





                    foreach (var item in blogs)
                    {
                        if(!item.seo_url.Contains("e-ticaret-sitesi-kurmak-icin-guncel-haberler-f620c8")  
                        && !item.seo_url.Contains("trendyolda-satis-nasil-yapilir") 
                        && !item.seo_url.Contains("turkiyenin-en-buyuk-e-ticaret-siteleri") 
                        && !item.seo_url.Contains("kitle-psikolojisi-nedir-d42ef7") 
                        && !item.seo_url.Contains("e-ticarette-hizli-teslimat-icin-ipuclari")
                        && !item.seo_url.Contains("online-pazaryeri-entegrasyonu-nedir")
                        && !item.seo_url.Contains("hepsiburadada-nasil-magaza-acilir")
                        && !item.seo_url.Contains("e-ticaret-nedir")
                        && !item.seo_url.Contains("trendyolda-nasil-magaza-acilir")
                        && !item.seo_url.Contains("dropshipping-nedir-stoksuz-e-ticaret-nasil-yapilir")
                        && !item.seo_url.Contains("shopify-ne-demektir")
                        && !item.seo_url.Contains("gelir-vergisi-nedir") 
                        && !item.seo_url.Contains("n11-nedir-pazaryerinde-nasil-magaza-acilir")
                        && !item.seo_url.Contains("en-basarili-ve-pratik-pazaryeri-entegrasyonlari"))
                        {
                            xml.WriteStartElement("url");
                            xml.WriteElementString("loc", "https://www.jetstok.com/blog/" + @item.seo_url);
                            xml.WriteEndElement();
                          }
                    }


                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", "https://www.jetstok.com/xml-entegrasyonlar");
                    xml.WriteEndElement();

                    foreach (var item in xmlIntegrations)
                    {
                        xml.WriteStartElement("url");
                        xml.WriteElementString("loc", "https://www.jetstok.com/xml-entegrasyon/" + @item.seo_url);
                        xml.WriteEndElement();
                    }

                    xml.WriteEndElement();
                }
                responseString = sw.ToString().Replace("utf-16", "utf-8");
            }


            return new ContentResult
            {
                ContentType = "application/xml",
                Content = responseString,
                StatusCode = 200
            };
        }
    }
}
