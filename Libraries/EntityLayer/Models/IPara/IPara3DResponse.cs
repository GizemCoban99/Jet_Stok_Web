namespace EntityLayer.Models.IPara
{
    public class IPara3DResponse : IParaBaseResponse
    {
        public string Amount { get; set; }

        public string OrderId { get; set; }

        public string PublicKey { get; set; }

        public string CommissionRate { get; set; }

        public string ThreeDSecureCode { get; set; }
    }
}
