﻿using Newtonsoft.Json;
using System.Xml.Serialization;

namespace EntityLayer.Models.IPara
{
    /// <summary>
    /// Bu sınıf 3D secure olmadan ödeme kısmında müşteri bilgilerinin kullanılacağı yerde ve
    ///  3D Secure ile Ödeme'nin 2. adımında müşteri bilgilerinin istendiği yerde kullanılır.
    /// </summary>
    [XmlRoot("purchaser")]
    public class IParaPurchaser
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        [XmlElement("surname")]
        public string SurName { get; set; }

        [XmlElement("birthDate")]
        public string BirthDate { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("gsmNumber")]
        public string GsmPhone { get; set; }

        [XmlElement("tcCertificate")]
        public string IdentityNumber { get; set; }

        [XmlElement("clientIp")]
        public string ClientIp { get; set; }

    }
}
