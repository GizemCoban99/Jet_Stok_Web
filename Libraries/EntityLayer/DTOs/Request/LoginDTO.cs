using FluentValidation;
namespace EntityLayer.DTOs.Request
{
    public class LoginDTO
    { 
        public string Email { get; set; }
         
        public string Password { get; set; }
        public string SmsCode { get; set; }
        public string SmsAuth { get; set; } 
    }

    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(p => p.Email).NotNull().WithMessage("Email boş olamaz.").MinimumLength(5).WithMessage("Email en az 5 karakter olmalıdır.").EmailAddress().WithMessage("Email formatını kontrol ediniz.");
            RuleFor(p => p.Password).NotNull().WithMessage("Şifre Boş olamaz.").MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
        }
    }
}
