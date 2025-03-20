using EntityLayer.DTOs.Request;
using FluentValidation;

namespace EntityLayer.DTOs
{
    public class ContactFormDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class InfoFormDTO
    {
        public string Name { get; set; } 
        public string Phone { get; set; }
        public string Source { get; set; }
    }

    public class ContactFormDTOValidator : AbstractValidator<ContactFormDTO>
    {
        public ContactFormDTOValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("İsim Boş olamaz.").MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır.");
            //RuleFor(p => p.Email).NotNull().WithMessage("Email boş olamaz.").MinimumLength(2).WithMessage("Email en az 5 karakter olmalıdır.").EmailAddress().WithMessage("Email formatını kontrol ediniz.");
            RuleFor(p => p.Phone).NotNull().WithMessage("Telefon Numarası Boş olamaz.").MinimumLength(10).WithMessage("Telefon numarası en az 10 karakter olmalıdır.");
            RuleFor(p => p.Title).NotNull().WithMessage("Başlık Boş olamaz.").MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır.");
            RuleFor(p => p.Description).NotNull().WithMessage("Açıklama Boş olamaz.").MinimumLength(2).WithMessage("Açıklama en az 2 karakter olmalıdır.");

        }
        
    }
    public class ContactFormDTOValidator2 : AbstractValidator<ContactFormDTO>
    { 
        public ContactFormDTOValidator2()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("İsim Boş olamaz.").MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır.");
            //RuleFor(p => p.Email).NotNull().WithMessage("Email boş olamaz.").MinimumLength(2).WithMessage("Email en az 5 karakter olmalıdır.").EmailAddress().WithMessage("Email formatını kontrol ediniz.");
            RuleFor(p => p.Phone).NotNull().WithMessage("Telefon Numarası Boş olamaz.").MinimumLength(10).WithMessage("Telefon numarası en az 10 karakter olmalıdır.");


        }
    }
}
