using FluentValidation;
using Library.Models;

namespace WebClient.Validation
{
    public class CampusRequestValidation : AbstractValidator<Campus>
    {
        public CampusRequestValidation()
        {
            this.RuleFor(v => v.CampusName)
                .NotEmpty().WithMessage("Campus name is required!")
                .NotNull().WithMessage("Campus name is required!")
                .MaximumLength(100).WithMessage("Campus name must less than 100 character!");
        }
    }
}
