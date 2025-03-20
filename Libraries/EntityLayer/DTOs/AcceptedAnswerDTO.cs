using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EntityLayer.DTOs
{
    public class AcceptedAnswerDTO
    {
        [JsonProperty(PropertyName = "@type")] 
        public string Type { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

    }
}
