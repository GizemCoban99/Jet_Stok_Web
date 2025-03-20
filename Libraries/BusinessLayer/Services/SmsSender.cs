using EntityLayer.DTOs.Request;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BusinessLayer.Services
{

    public static class SmsSender
    {
        private static string sendUrl = "http://sms.verimor.com.tr/v2/send.json";
        private static string username = "902167065538";
        private static string password = "Asd123.!*.1";

        public static bool Send(Message[] messages)
        {
            var sms = new SendMessage()
            {
                messages = messages,
                password = password,
                username = username
            };

            string payload = JsonConvert.SerializeObject(sms);

            using (WebClient wc = new WebClient())
            {

                wc.Encoding = Encoding.UTF8;
                wc.Headers["Content-Type"] = "application/json";

                try
                {
                    string campaign_id = wc.UploadString(sendUrl, payload);
                    return true;
                }
                catch (WebException ex) // 400 hatalarında response body'de hatanın ne olduğunu yakalıyoruz
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError) // 400 hataları
                    {
                        var responseBody = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();

                    }
                    else // diğer hatalar
                    {
                    }
                }
            }

            return false;
        }
    }
}
