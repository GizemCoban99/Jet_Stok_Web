using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text; 
using ImageMagick;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using CoreLayer.Enum;

namespace CoreLayer
{
    public static class Globals
    {

        public static Dictionary<int, string> BasicDateFilterList = new Dictionary<int, string>() { { 0, "Choice" }, { 1, "Today" }, { 2, "This Month" }, { 3, "This Year" }, { 4, "Last Year" } };

        /// <summary>
        /// Parola şifreleme için çalışır
        /// </summary>
        /// <param name="data"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string DataEncription(string data, string email)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: data,
            salt: Encoding.UTF8.GetBytes(email),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

            return hashed;
        }

        /// <summary>
        /// giriş için sms kodu üretir.
        /// </summary>
        /// <returns></returns>
        public static string GenerateSmsCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private static string GetDescriptionFromEnumValue<T>(string valueString)
        {
            try
            {
                var value = (T)MarketPlaceEnum.Parse(typeof(T), valueString, true);
                DescriptionAttribute attribute = value.GetType()
                     .GetField(value.ToString())
                     .GetCustomAttributes(typeof(DescriptionAttribute), false)
                     .SingleOrDefault() as DescriptionAttribute;

                return attribute != null && !string.IsNullOrEmpty(attribute.Description) ? attribute.Description : value.ToString();
            }
            catch (Exception)
            {
                return valueString;
            }
        }

        public static string ClearHtml(string input)
        {
            try
            {

                return Regex.Replace(input, "<.*?>", String.Empty);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetEnumString<T>(int status)
        {
            try
            {
                var result = "";
                result = GetDescriptionFromEnumValue<T>(status.ToString());


                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Substring işlemlerinde kullanılır. 0 dan başlar kaç tane keseceğini alır ve işlemi yapar.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSubstring(string value, int length)
        {
            try
            {
                return value.Length > length ? value.Substring(0, length) : value;
            }
            catch (Exception ex)
            {
                return value;
            }
        }

        

        public static string GetImageUrl(string image)
        {
            if (!string.IsNullOrEmpty(image))
            {
                if (image.Contains("https://") || image.Contains("http://"))
                {
                    return image;
                }
            }
            else
            {
                return "";
            }
            return "https://image.hanxgame.com/" + image;
            //return "~/assets/images" + image;
        }
         
        
        public static string FileUpload(IFormFile file,long companyId, int contentId, string imgName = "")
        {
            var path = companyId + "/" + contentId;
            var base64 = file.ConvertToBase64String();
            string filePath = "";
            if (base64 != null && base64.Length > 0 && !string.IsNullOrEmpty(path) && path.Split('/').Length > 0)
            {
                var bytes = Convert.FromBase64String(base64);// a.base64image 
                using (MagickImage image = new MagickImage(bytes))
                {
                    bytes = Convert.FromBase64String(Globals.MagickImageWrite(image));
                }
             
           
            }
            return filePath.Replace("../home/sftp_user/", "");
        }

        private static string MagickImageWrite(MagickImage imageMagick)
        {
            try
            {
                if (!MarketPlaceEnum.IsDefined(typeof(MagickFormat), imageMagick.Format))
                {
                    imageMagick.Format = MagickFormat.Jpeg;
                }

                if (imageMagick.Width > 1400)
                {
                    var say = 90;
                    while (imageMagick.Width > 1400)
                    {
                        if (Convert.ToInt32((imageMagick.Width / 100) * say) < 1400)
                        {
                            imageMagick.Resize(Convert.ToInt32((imageMagick.Width / 100) * say), Convert.ToInt32((imageMagick.Height / 100) * say));
                        }
                        say -= 10;
                    }
                }
                string base64 = Convert.ToBase64String(imageMagick.ToByteArray());
                if (base64.Length > 600000)
                {
                    var say = imageMagick.Quality;

                    while (base64.Length > 600000)
                    {
                        imageMagick.Quality = say;
                        base64 = Convert.ToBase64String(imageMagick.ToByteArray());
                        if (say > 70)
                        {
                            say -= 10;
                        }
                        else
                        {
                            break;
                        }

                    }
                }


                return base64;
            }
            catch (Exception ex)
            {
            }
            return "";
        }

       

        public static string GetDescriptionFromEnumValue(MarketPlaceEnum value)
        {
            try
            {
                DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
                return attribute == null ? value.ToString() : attribute.Description;
            }
            catch
            {
                return null;
            }

        }

        public static string RandomNumber(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string ToMarketPlaceNameString(this int value)
        {
            var enumerationValue = (MarketPlaceEnum)value;
            return enumerationValue.ToString();
        }
        public static string ToMarketPlaceImageString(this int value)
        {
            return "assets/marketplaces/" + value.ToMarketPlaceNameString().ToLower() + ".png";
        }
    }
}
