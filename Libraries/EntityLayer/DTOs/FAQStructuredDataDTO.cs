using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntityLayer.DTOs
{
    public class FAQStructuredDataDTO
    {

        [JsonProperty(PropertyName = "@type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "acceptedAnswer")]
        public AcceptedAnswerDTO AcceptedAnswer { get; set; }
    }
}
