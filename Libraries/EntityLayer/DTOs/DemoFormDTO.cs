using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs
{
    public class DemoFormDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [MinLength(10)]
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string Source { get; set; }

     
    }

    public class DemoFormDTOValidator : AbstractValidator<DemoFormDTO>
    {
        public DemoFormDTOValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("İsim Boş olamaz.").MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır.");
            RuleFor(p => p.Name).NotNull().WithMessage("Soyad Boş olamaz.").MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.");
            //RuleFor(p => p.Email).NotNull().WithMessage("Email boş olamaz.").MinimumLength(2).WithMessage("Email en az 5 karakter olmalıdır.").EmailAddress().WithMessage("Email formatını kontrol ediniz.");
            RuleFor(p => p.Phone).NotNull().WithMessage("Telefon Numarası Boş olamaz.").MinimumLength(10).WithMessage("Telefon numarası en az 10 karakter olmalıdır.");

        }

    }

}
