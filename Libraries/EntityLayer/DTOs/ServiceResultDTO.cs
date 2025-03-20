namespace EntityLayer.DTOs
{
    public class ServiceResultDTO<T> where T : class
    {
        public ServiceResultDTO(bool isSuccess = true, string message = "", List<string> messages = null, T data = null, List<T> datas = null, int _total_rows = 0, Exception exception = null, string redirectUrl = "")
        {
            IsSuccess = exception == null ? isSuccess : false;
            Message = exception == null ? message : exception.Message;
            Messages = messages;
            Data = data;
            Datas = datas;
            total_rows = _total_rows;
            RedirectUrl = redirectUrl;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }
        public List<T> Datas { get; set; }
        public int total_rows { get; set; }
        public string RedirectUrl { get; set; }
    }
}
