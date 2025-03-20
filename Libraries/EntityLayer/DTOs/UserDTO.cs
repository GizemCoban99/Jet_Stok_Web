using FluentValidation;
using Microsoft.AspNetCore.Http; 

namespace EntityLayer.DTOs
{
    public class UserDTO
    {
        public int id { get; set; } 
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? last_login_date { get; set; } 
        public string branch_name { get; set; }
        public string phone { get; set; }
        public int role_id { get; set; }
        public string role_name { get; set; }
        public string tc_number { get; set; }
        public string image { get; set; }
        public IFormFile profile_image { get; set; }
         
        public string new_password { get; set; }
        public string new_password_check { get; set; }

        public class UserDTOValidator : AbstractValidator<UserDTO>
        {
            public UserDTOValidator()
            {

                RuleFor(p => p.name).NotEmpty().WithMessage("His name can't be empty.").MinimumLength(3).WithMessage("The name must be at least 3 characters.");

                RuleFor(p => p.surname).NotEmpty().WithMessage("The surname can't be empty.").MinimumLength(3).WithMessage("Soyadı en az 3 karakter olmalıdır.");

                RuleFor(p => p.email).NotEmpty().WithMessage("The email can't be empty.").MinimumLength(3).WithMessage("The e-mail must be at least 3 characters.").EmailAddress().WithMessage("Please check the E-Mail address.");

                RuleFor(p => p.role_id).GreaterThanOrEqualTo(1).WithMessage("The role can't be empty.");

                RuleFor(p => p.phone).NotEmpty().WithMessage("The phone can't be empty.").Length(10).WithMessage("The phone number should have 10 characters without 0 at the beginning. It can contain only numerical values.").Matches("^[0-9]*$").WithMessage("The phone number should have 10 characters without 0 at the beginning. It can contain only numerical values.");

                RuleFor(p => p.new_password).MinimumLength(6).WithMessage("The password must be at least 6 characters.");

                RuleFor(p => p.new_password).Equal(p => p.new_password_check).WithMessage("Check the passwords.");

                RuleFor(p => p.tc_number).Length(11).WithMessage("The Identification number must be 11 characters.").Matches("^[0-9]*$").WithMessage("The Identification number can only take a numerical value.");
            }
        }
    }
}
