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

            this.RuleFor(v => v.UserName)
                .NotEmpty().WithMessage("Name is required!")
                .NotNull().WithMessage("Name is required!")
                .MaximumLength(100).WithMessage("Name must less than 100 character!");

            this.RuleFor(v => v.Phone)
                .MaximumLength(11).WithMessage("Phone number must less than 11 number")
                .Matches(@"^[0-9]*$").When(e => !string.IsNullOrEmpty(e.Phone)).WithMessage("Phone number must be a number")
                .MinimumLength(8).When(e => !string.IsNullOrEmpty(e.Phone)).WithMessage("Phone number must at least 8 number");

            this.RuleFor(v => v.RoleId)
                .NotEmpty().WithMessage("Role is required!")
                .NotNull().WithMessage("Role is required!");

            this.RuleFor(v => v.CampusId)
                .NotEmpty().WithMessage("Campus is required!")
                .NotNull().WithMessage("Campus is required!");
        }


    }
}
