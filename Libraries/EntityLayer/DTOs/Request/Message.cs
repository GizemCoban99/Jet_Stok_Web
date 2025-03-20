namespace EntityLayer.DTOs.Request
{
    public class Message
    {
        public string msg { get; set; }
        public string dest { get; set; }

        public Message() { }

        public Message(string msg, string dest)
        {
            this.msg = msg;
            this.dest = dest;
        }
    }
}
