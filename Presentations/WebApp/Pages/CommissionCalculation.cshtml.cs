using AutoMapper;
using BusinessLayer.Services;
using BusinessLayer.Services.Marketplaces;
using CoreLayer;
using CoreLayer.Enum;
using EntityLayer.DTOs;
using EntityLayer.DTOs.Component.Select2;
using GraphQL.Client.Abstractions.Utilities;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class CommissionCalculationModel : _BasePageModel
    {
        public string pageUITitle { get; set; }
        public string pageUIContent { get; set; }
        public string pageUIContent2 { get; set; }
        public string marketString { get; set; }

        public CommissionCalculationDTO commissionCalculationForm = new CommissionCalculationDTO();

        private readonly MarketplacesService _marketPlacesService;
        private readonly MarketplaceCommissionRateService _marketPlaceCommissionRateService;
        private readonly MarketplaceOperationRateService _marketPlaceOperationRateService;
        public CommissionCalculationModel(IMapper mapper, GenericValueService genericValueService, MarketplacesService marketPlacesService, MarketplaceCommissionRateService marketPlaceCommissionRateService, MarketplaceOperationRateService marketPlaceOperationRateService) : base(mapper, genericValueService)
        {
            _marketPlacesService = marketPlacesService;
            _marketPlaceCommissionRateService = marketPlaceCommissionRateService;
            _marketPlaceOperationRateService = marketPlaceOperationRateService;
        }

        public void OnGet(string market)
        {
            pageTitle = "Pazaryeri Komisyon Hesaplama - Jet Stok";
            pageDescription = "Komisyon Hesaplama: Pazaryeri kategorinizi seçerek komisyon hesaplamınızı kolaylıkla yapın. Kazançlarınızı net olarak hesaplayın.";
            pageCanonical = "https://www.jetstok.com/komisyon-hesaplama";
            pageUITitle = "Pazaryeri Komisyon Hesaplama";
            pageUIContent = "Kazançlarınızı arttırmak için komisyonunuzu hesaplayın ve net olarak gelirlerinizi yönetin.";

            pageUIContent2 = $"<h2>Pazaryeri komisyonu nedir?</h2><br> E-ticaretin artan popülaritesi nedeniyle birçok çevrimiçi pazaryeri oluşturulmuştur. Bu pazar yerlerinin çoğu benzer şekilde çalışır; satıcılar mağazalar kurar, ürünlerini çevrimiçi olarak paylaşır ve bunları web veya mobil uygulamalar aracılığıyla ilgilenen müşterilere sunar. <br><br> Birçok pazaryeri, farklı kategorilerdeki ürün satışları için standartlaştırılmış komisyon oranlarına sahiptir ve bu oranları, satıcılar tarafından yapılan KDV dahil satışlardan düşmektedir. Bununla birlikte, bazı pazaryerleri satıcılara nakliye anlaşmalarından yararlanma ve ürünlerini göndermek için daha düşük bir fiyat alma fırsatı sunmaktadır. <br><br> Bazı pazaryerlerinde ücretsiz kargo mevcut olsa da bazı pazaryerleri ise satıcıların kendisi farklı bir kargo yöntemine karar verebilmektedir. Pazaryeri için (Trendyol, Hepsiburada ve N11 gibi Türkiye'nin diğer büyük pazaryerleri) kategori bazlı komisyonunuzu hesaplamak istiyorsanız Jet Stok Komisyon Hesaplama alanından hesaplayabilirsiniz. Ancak hesaplama yapmadan önce, karlılık analizi yapılırken bu durumun dikkate alınması gerektiğini, ürününüzün satış fiyatları hesaplanırken kargo masrafları ve komisyon ücretlerinin de dikkate alınması gerektiğini belirtmekte fayda var.<br><br>";

            pageUIContent2 += $"<h2>Komisyon nasıl hesaplanır?</h2><br> Pazaryeri tarafından satışı gerçekleşen ürünlerin satış fiyatı üzerinden Hizmet ve Komisyon bedeli altında kategori bazından farklı oranlarda alınan giderdir. Bu gider sizde kesilerek size faturası iletilir. <br><br>";
            pageUIContent2 += $"<h2>Kar nasıl hesaplanır?</h2><br> Pazaryeri kar hesaplamak için öncelikle ürünün tüm giderlerini kalem kalem not etmeniz gerekmektedir. Bunları satış fiyatı üzerinden çıkarttığınız zaman Pazaryeri ettiğiniz karı bulabilirsiniz.<br><br>";
            pageUIContent2 += $"<h2>Komisyon Neden Alınır?</h2><br> Ticari bir faaliyetin gerçekleşmesi halinde, işe aracılık eden kişi veya kuruluşa bir miktar para ödenebilir. Kişi veya kuruluş işe yardım ettiği için bu ücret ödenmektedir.  Bir pazar yeri aracılığıyla satış yapıyorsanız ve hizmetlerini müşteri tabanına fayda sağlamak için kullanıyorsanız, işletmenizden komisyon alınacaktır.<br><br>";
            pageUIContent2 += $"<h2>Komisyon Nasıl Belirlenir?</h2><br> Pazaryerleri, işletmelerin satış karşılığında aldıkları komisyon ücretini ürün grubuna, aylık ciroya veya ürün satış hacmine göre belirler. Bu satış sisteminin amacı yeni satış yapmaya başlayan satıcıları korumaktır.<br><br> E-ticarete başlamadan önce pazaryeri komisyon oranlarının belirlenmesi, satış yapmayı düşünen işletmeler için büyük önem taşıyor. E-ticarette dikkatli stratejik planlama yapmak önemlidir. Başarı için gereken adımları emin atmak için; komisyon oranları, ürün adetleri gibi bilgilerden yararlanılarak, gelir-gider kırılımını anlayarak sağlanabilir. Ek olarak: Birçok işletme, dalgalanan piyasa komisyon oranları nedeniyle kârlarının bir kısmını kaybeder. <br><br>";

            if (!string.IsNullOrEmpty(market))
            {
                market = market.Trim().ToPascalCase();
                var marketData = Enum.GetValues<MarketPlaceEnum>().FirstOrDefault(f => f.ToString().ToLower() == market.ToLower()).GetEnumValue();
                if (marketData > 0)
                {
                    var marketDesc = Enum.GetValues<MarketPlaceEnum>().FirstOrDefault(f => f.ToString().ToLower() == market.ToLower()).ToDescriptionString();
                    pageTitle = marketDesc + " Komisyon Hesaplama - Jet Stok";
                    pageDescription = marketDesc + " Komisyon Hesaplama: " + marketDesc + " kategorinizi seçerek komisyon hesaplamınızı kolaylıkla yapın. " + marketDesc + " kazançlarınızı net olarak hesaplayın.";
                    pageCanonical = $"https://www.jetstok.com/{market.ToLower()}-komisyon-hesaplama";
                    pageUITitle = marketDesc + " Komisyon Hesaplama";
                    pageUIContent = marketDesc + " komisyonunuzu hesaplayarak kazançlarınızı arttırmak için komisyonunuzu hesaplayın ve net olarak gelirlerinizi yönetin.";
                    pageUIContent2 = $"<b>{marketDesc} komisyonu nedir?</b><br> {marketDesc} komisyonu pazaryerinin bize sunduğu hizmetin karşılığında aldığı komisyondur. <br><br>";
                    pageUIContent2 += $"<b>{marketDesc} komisyonu nasıl hesaplanır?</b><br> {marketDesc} tarafından satışı gerçekleşen ürünlerin satış fiyatı üzerinden Hizmet ve Komisyon bedeli altında kategori bazından farklı oranlarda alınan giderdir. Bu gider sizde kesilerek size faturası iletilir. <br><br>";
                    pageUIContent2 += $"<b>{marketDesc} kar hesaplama</b><br> {marketDesc} kar hesaplamak için öncelikle ürünün tüm giderlerini kalem kalem not etmeniz gerekmektedir. Bunları satış fiyatı üzerinden çıkarttığınız zaman {marketDesc} ettiğiniz karı bulabilirsiniz.";

                    marketString = marketDesc +" Komisyon";
                }

            }
        }

        public JsonResult OnGetSearchCategories(string search, int marketPlace)
        {
            var result = new Select2Response();
            if (marketPlace > 0)
            {
                var categories = _marketPlacesService.SearchCategories(search, marketPlace).Data;
                result = new Select2Response() { results = categories != null ? categories.Select(s => new Select2ResponseItem { text = s.path, id = s.category_id }).ToList() : new List<Select2ResponseItem>() };
            }

            return new JsonResult(result);
        }
        public JsonResult OnGetCategoryCommission(int category_id)
        {
            decimal commision_rate = 0;
            var commision = _marketPlaceCommissionRateService.Get(category_id).Data;
            if (commision != null)
            {
                commision_rate = commision.commision_rate * 100;
            }
            return new JsonResult(commision_rate);
        }
        public JsonResult OnGetCiceksepetiListingPrice(int marketplace_id)
        {
            decimal listing_price = 0;
            var operation_rate = _marketPlaceOperationRateService.Get(marketplace_id).Data;
            if (operation_rate != null)
                listing_price = operation_rate.listing_price;

            return new JsonResult(operation_rate);
        }

        public JsonResult OnGetCommissionCalculation(int marketplace_id, decimal commission, string sale_price, string purchase_price, decimal kdv, string cargo_price, int cargo_type)
        {
            var model = new CommissionCalculationResponseDTO();
            if (marketplace_id == 0 || commission == 0 || (!string.IsNullOrEmpty(sale_price) && sale_price == "0") || string.IsNullOrEmpty(sale_price) || (!string.IsNullOrEmpty(purchase_price) && purchase_price == "0") || string.IsNullOrEmpty(purchase_price) || kdv == 0 || cargo_type == 0)
            {
                model.success = false;
                return new JsonResult(model);
            }

            var salePrice = sale_price.ConvertToDecimal();
            var purchasePrice = purchase_price.ConvertToDecimal();
            var cargoPrice = cargo_price.ConvertToDecimal();

            decimal proccessing_price = 0;
            decimal operation_price = 0;

            var operation_rate = _marketPlaceOperationRateService.Get(marketplace_id).Data;
            if (operation_rate != null)
            {
                proccessing_price = operation_rate.operation_price;
                operation_price = operation_rate.listing_price;
            }

            decimal commissionRate = (salePrice * commission) / 100;
            decimal price_without_purchase_price = salePrice - purchasePrice;
            var price_without_cargo_price = price_without_purchase_price - cargoPrice;
            decimal price_without_commission = (price_without_cargo_price - commissionRate) - operation_price;
            decimal price_without_processing_price = price_without_commission - (decimal)proccessing_price;
            decimal price_without_kdv = price_without_processing_price / (1 + kdv / 100);

            model = new CommissionCalculationResponseDTO()
            {
                commission = commissionRate.ToString("0.##"),
                price = price_without_kdv.ToString("0.##"),
                kdv_price = (price_without_processing_price - (price_without_processing_price / (1 + kdv / 100))).ToString("0.##"),
                service_price = (proccessing_price).ToString("0.##"),
                percentage_gain = ((price_without_kdv) * (100 / purchasePrice)).ToString("0.##"),
                success = true
            };

            return new JsonResult(model);
        }
    }
}