using Newtonsoft.Json;
using System.Xml.Serialization;

namespace EntityLayer.Models.IPara
{
    public class IParaProduct
    {
        [JsonProperty("productCode")]
        [XmlElement("productCode")]
        public string Code { get; set; }

        [JsonProperty("productName")]
        [XmlElement("productName")]
        public string Title { get; set; }

        [JsonProperty("quantity")]
        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("price")]
        [XmlElement("price")]
        public string Price { get; set; }
    }
}
