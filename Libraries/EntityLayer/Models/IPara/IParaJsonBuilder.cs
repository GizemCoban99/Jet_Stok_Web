using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text;

namespace EntityLayer.Models.IPara
{
    public class IParaJsonBuilder
    {
        public static string SerializeToJsonString(IParaBaseRequest request)
        {
            return JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static StringContent ToJsonString(IParaBaseRequest request)
        {
            return new StringContent(SerializeToJsonString(request), Encoding.Unicode, "application/json");
        }
    }
}
