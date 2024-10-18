using FluentValidation;
using Library.Request;
using Library.Response;

namespace WebClient.Validation
{
    public class AdminCreateUserValidation : AbstractValidator<UserRequest>
    {
        public AdminCreateUserValidation()
        {
            this.RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required!")
                .NotNull().WithMessage("Email is required!")
                .MaximumLength(100).WithMessage("Email must less than 100 character!");

            this.RuleFor(v => v.RoleId)
                .NotEmpty().WithMessage("Role is required!")
                .NotNull().WithMessage("Role is required!");

            this.RuleFor(v => v.CampusId)
                .NotEmpty().WithMessage("Campus is required!")
                .NotNull().WithMessage("Campus is required!");
        }
    }
}
