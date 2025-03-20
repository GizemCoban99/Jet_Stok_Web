namespace EntityLayer.DTOs.Request
{
    public class SendMessage
    {
        public string username { get; set; }
        public string password { get; set; }
        public string source_addr { get; set; }
        public Message[] messages { get; set; }
    }
}
