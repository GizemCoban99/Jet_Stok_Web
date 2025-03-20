using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.DTOs
{
    public class RegisterFormDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [MinLength(10)]
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string Source { get; set; }
        public string Password { get; set; }
        public long PackageId { get; set; }
    }

    public class RegisterFormDTOValidator : AbstractValidator<RegisterFormDTO>
    {
        public RegisterFormDTOValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("İsim Boş olamaz.").MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır.");

            RuleFor(p => p.Surname).NotNull().WithMessage("Soyad Boş olamaz.").MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.");

            RuleFor(p => p.Email).NotNull().WithMessage("Email boş olamaz.").MinimumLength(2).WithMessage("Email en az 5 karakter olmalıdır.").EmailAddress().WithMessage("Email formatını kontrol ediniz.");

            RuleFor(p => p.Phone).NotNull().WithMessage("Telefon Numarası Boş olamaz.").MinimumLength(10).WithMessage("Telefon numarası en az 10 karakter olmalıdır.")
                .MaximumLength(10).WithMessage("Telefon numarası en az 10 karakter olmalıdır.");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(10).WithMessage("Şifre en az 10 karakter olmalıdır.");
        }
    }
}
