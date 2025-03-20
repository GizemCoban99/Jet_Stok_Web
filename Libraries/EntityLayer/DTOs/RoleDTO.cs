using FluentValidation;

namespace EntityLayer.DTOs
{
    public class RoleDTO
    {
        public int id { get; set; } 
        public string name { get; set; }
        public string permissions { get; set; }
        public int[] permission_array { get; set; } 
        public string description { get; set; }
    }
    public class RoleDTOValidator : AbstractValidator<RoleDTO>
    {
        public RoleDTOValidator()
        {
            RuleFor(p => p.name).NotEmpty().WithMessage("The role name can't be empty.").MinimumLength(2).WithMessage("The role name must be at least 2 characters."); 
        }
    }
}
