using FluentValidation;

namespace EntityLayer.Models
{
    public class PaymentForm
    {
        public string guid{ get; set; }

        public string SecurityCode { get; set; }

        public string ExpirationDate { get; set; }

        public string CardNumber { get; set; }

        public string Name { get; set; }
    }
    public class PaymentFormValidator : AbstractValidator<PaymentForm>
    {
        public PaymentFormValidator()
        {
            RuleFor(p => p.SecurityCode).NotNull().WithMessage("cvv bilgisi boş olamaz").NotEmpty().WithMessage("cvv bilgisi boş olamaz");
            RuleFor(p => p.ExpirationDate).NotEmpty().WithMessage("Tarih boş olamaz").NotNull().WithMessage("Tarih boş olamaz");
            RuleFor(p => p.CardNumber).NotEmpty().WithMessage("Kart numarası boş olamaz").NotNull().WithMessage("Kart numarası boş olamaz");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Kart üzerindeki isim boş olamaz").NotNull().WithMessage("Kart üzerindeki isim boş olamaz");
        }
    }
}
