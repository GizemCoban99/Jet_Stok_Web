using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreLayer.Enum
{
    public enum MarketPlaceEnum
    {
        [Description("N11")]
        [Display(Prompt = "~/assets/marketplaces/n11.png")]
        N11 = 1,
        [Description("Trendyol")]
        [Display(Prompt = "~/assets/marketplaces/trendyol.png")]
        Trendyol = 2,
        [Description("Hepsiburada")]
        [Display(Prompt = "~/assets/marketplaces/hepsiburada.png")]
        Hepsiburada = 3,
        [Description("Çiçeksepeti")]
        [Display(Prompt = "~/assets/marketplaces/ciceksepeti.png")]
        CicekSepeti = 10,
        [Description("EPttAvm")]
        [Display(Prompt = "~/assets/marketplaces/pttavm.png")]
        PttAvm = 15
    }
}
