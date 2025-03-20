using CoreLayer.Enum;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CoreLayer
{
    public static class Extensions
    {
        public static string ToDescriptionString(this MarketPlaceEnum enumerationValue)
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }
        public static int GetEnumValue(this MarketPlaceEnum enumerationValue)
        {
            return (int)(object)enumerationValue;
        }

        public static string ToPromptString(this MarketPlaceEnum enumerationValue)
        {

            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DisplayAttribute)attrs[0]).Prompt;
                }
            }
            return enumerationValue.ToString();
        }


        public static long ToLong(this string value)
        {
            try
            {
                if (String.IsNullOrEmpty(value))
                    return 0;
                else
                    return Convert.ToInt64(value);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static Int32 ToInt32(this string value)
        {
            try
            {
                if (String.IsNullOrEmpty(value))
                    return 0;
                else
                    return Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }



        /// <summary>
        /// Gönderilen bir tarihi metin olarak şimdi, 1 gün önce gibi sonuçlara dönüştürür
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDateTimeFriendlyString(this DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }
            TimeSpan timeDifference = DateTime.Now - date;
            int second = Convert.ToInt32(Math.Round(timeDifference.TotalSeconds));
            int minute = Convert.ToInt32(Math.Round(timeDifference.TotalSeconds / 60));
            int hour = Convert.ToInt32(Math.Round(timeDifference.TotalSeconds / 3600));
            int day = Convert.ToInt32(Math.Round(timeDifference.TotalSeconds / 86400));
            int week = Convert.ToInt32(Math.Round(timeDifference.TotalSeconds / 604800));
            int month = Convert.ToInt32(Math.Round(timeDifference.TotalSeconds / 2419200));
            int year = Convert.ToInt32(Math.Round(timeDifference.TotalSeconds / 29030400));

            if (second <= 59)
                if (second == 0)
                {
                    return "Now";
                }
                else
                {
                    return second + " seconds ago";
                }
            else if (minute <= 59)
                return minute + " minutes ago";
            else if (hour <= 23)
                return hour + " hours ago";
            else if (day <= 30)
                return day + " days ago";
            else if (month <= 11)
                return month + " months ago";
            else
                return year + " years ago";

        }
        /// <summary>
        /// Paket kullanım kalan süresi hesaplar.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDateTimeRemainderFriendlyString(this DateTime date)
        {
            try
            {
                var result = "";
                if (date < DateTime.Now)
                    result = "-";
                else
                {
                    TimeSpan timeDifference = DateTime.Now - date;
                    int hour = Math.Abs(Convert.ToInt32(Math.Round(timeDifference.TotalSeconds / 3600)));
                    int day = Math.Abs(Convert.ToInt32(Math.Round(timeDifference.TotalSeconds / 86400)));
                    if (day == 0)
                        result = "<span>Paket sürenizin sona ermesine <strong>" + hour + "</strong> saat kaldı.</span>";
                    else if (day > 30)
                        result = "<span>Paket süreniz <strong>" + date.GetDateStringFormat() + "</strong> tarihinde sona erecek.</span>";
                    else
                        result = "<span>Paket sürenizin sona ermesine <strong>" + day + "</strong> gün kaldı.</span>";
                }
                return result;
            }
            catch (Exception ex)
            {
                return date.GetDateTimeStringFormat();
            }

        }

        /// <summary>
        /// Verilen ayın Türkçe ismini verir
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GetMonthName(this int month)
        {
            try
            {
                return new CultureInfo("tr-TR").DateTimeFormat.GetMonthName(month);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// gelen tarihi ortak tarih formatına dönüştürür
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDateTimeStringFormat(this DateTime date)
        {
            if (date == DateTime.MinValue)
                return "-";

            return date.ToString("dd/MM/yyyy HH:mm");
        }
        /// <summary>
        /// gelen tarihi ortak tarih formatına dönüştürür.sadece tarih
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDateStringFormat(this DateTime date)
        {
            if (date == DateTime.MinValue)
                return "-";

            return date.ToString("dd/MM/yyyy");
        }
        /// <summary>
        /// Parasal değerleri doğru bir şekilde string olarak göstermeye yarar
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToDecimal(this decimal value)
        {
            try
            {
                if (value != null)
                {
                    string valString = value.ToString("N");
                    decimal val;
                    var price = "";
                    if (!decimal.TryParse(valString, NumberStyles.Number, CultureInfo.InvariantCulture, out val))
                    {
                        price = "0,00 €";
                    }

                    valString = valString;

                    return Decimal.Parse(price != "" ? price : valString).ToString("N");
                }
                else
                {
                    return "0,00 €";
                }


            }
            catch (Exception)
            {
                return "0,00 €";
            }
        }

        public static string DecimalToStringWithSymbol(this decimal value)
        {
            try
            {
                var culture = new CultureInfo("en-FR");
                culture.NumberFormat.CurrencySymbol = "€";
                culture = (CultureInfo)culture.Clone();
                culture.NumberFormat.CurrencyPositivePattern = 3;
                culture.NumberFormat.CurrencyNegativePattern = 3;
                return value.ToString("C", culture);

            }
            catch (Exception)
            {
                return "0,00 €";
            }
        }

        /// <summary>
        /// Parasal değerleri doğru bir şekilde string olarak göstermeye yarar
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(this string value)
        {
            try
            {
                if (value != null)
                {
                    return Convert.ToDecimal(value);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal ConvertToDecimalTwoDigit(this decimal value)
        {
            try
            {
                return Math.Floor(value * 100) / 100;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static DateTime ConvertToDateTime(this string value)
        {
            try
            {
                if (value != null)
                {
                    return Convert.ToDateTime(value);
                }
                else
                {
                    return default;
                }
            }
            catch (Exception)
            {
                return default;
            }
        }
        public static string DecimalToString(this decimal price)
        {
            try
            {
                string value = price.ToString("n2");
                value = value.Replace(",", ":").Replace(".", ",").Replace(":", ".");
                return value;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string StringPriceReplace(this string price)
        {
            try
            {
                price = price.Replace(",", ":").Replace(".", ",").Replace(":", ".");
                return price;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string ConvertToDecimalTwoDigitFormFormat(this decimal value)
        {
            try
            {
                return value.ToString("n2");
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static string ClearToSqlFullLikeQuery(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return "";
                }
                value = value.Replace("[", "[[]").Replace("%", "[%]");
                return "%" + value + "%";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string ConvertToDecimalTrFormat(this decimal value)
        {
            try
            {
                var result = Math.Floor(value * 100) / 100;

                var cultureInfo = CultureInfo.GetCultureInfo("de-DE");
                return result.ToString("c", cultureInfo);
            }
            catch (Exception)
            {
                return value.ToString();
            }

        }
        public static string ClearToSqlStartLikeQuery(this string value)
        {
            try
            {
                value = value.Replace("[", "[[]").Replace("%", "[%]");
                return "%" + value;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string ClearToSqlEndLikeQuery(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return value;

                value = value.Replace("[", "[[]").Replace("%", "[%]");
                return value + "%";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetEnglishMonthName(this int value)
        {
            try
            {
                CultureInfo usEnglish = new CultureInfo("en-US");
                DateTimeFormatInfo englishInfo = usEnglish.DateTimeFormat;
                return englishInfo.MonthNames[value - 1];
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string ConvertToBase64String(this IFormFile file)
        {
            try
            {

                if (file != null && file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        return Convert.ToBase64String(fileBytes);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            JToken token = metaToken as JToken;
            if (token == null)
            {
                return ToKeyValue(JObject.FromObject(metaToken));
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                                                 .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date ?
                            jValue?.ToString("o", CultureInfo.InvariantCulture) :
                            jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }
        public static string ToImagePath(this string value)
        {
            var domain = "";
            #if DEBUG
                        domain = "https://www.jetstok.com";
            #endif
            return $"{domain}/upload/{value}";
        }

    }
}
