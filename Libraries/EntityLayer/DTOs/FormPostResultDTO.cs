using FluentValidation.Results;

namespace EntityLayer.DTOs
{
    public class FormPostResultDTO<T> where T : class
    {
        public FormPostResultDTO(bool isSuccess=true, string successMessage = "", List<string> errors=null, T data=null, string title="", string okButton="", string redirectUrl="")
        {
            this.isSuccess = isSuccess;
            this.successMessage = successMessage;
            this.errors = errors==null?new List<string>(): errors;
            this.data = data;
            this.title = title;
            this.okButton = okButton;
            this.redirectUrl=redirectUrl;
        }
         
        public FormPostResultDTO(ValidationResult validation)
        {
            this.isSuccess = false;
            errors = new List<string>();
            foreach (var failure in validation.Errors)
            {
                errors.Add(failure.ErrorMessage);
            }
            this.title = "Please check the information.";
            this.okButton = "OK";
        }


        public FormPostResultDTO(Exception ex)
        {
            this.isSuccess = false;
            errors = new List<string>() { ex.Message}; 
            this.title = "Please check the information.";
            this.okButton = "OK";
        }
        public FormPostResultDTO(string errorMessage)
        {
            this.isSuccess = false;
            errors = new List<string>() { errorMessage };
            this.title = "Please check the information.";
            this.okButton = "OK";
        }

        public FormPostResultDTO(string errorMessage, bool isPriceControl, string redirectUrl, long product_id, int marketplace)
        {
            this.isSuccess = false;
            errors = new List<string>() { errorMessage };
            this.title = "Please check the information.";
            this.redirectUrl = redirectUrl;
            this.okButton = "OK";
            this.errorData = new { isPriceControl, productId = product_id, marketplace };
        }

        public bool isSuccess { get; set; }
        public string title { get; set; }
        public string okButton { get; set; }
        public string redirectUrl { get; set; }
        public string successMessage { get; set; }
        public List<string> errors { get; set; }
        public T data { get; set; }
        public object errorData { get; set; }
    }
}
