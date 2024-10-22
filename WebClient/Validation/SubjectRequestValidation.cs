using FluentValidation;
using Library.Models;

namespace WebClient.Validation
{
    public class SubjectRequestValidation : AbstractValidator<Subject>
    {
        public SubjectRequestValidation()
        {
            this.RuleFor(v => v.SubjectCode)
                .NotEmpty().WithMessage("Subject code is required!")
                .NotNull().WithMessage("Subject code is required!")
                .MaximumLength(10).WithMessage("Subject code must less than 10 character!");

            this.RuleFor(v => v.SubjectName)
                .NotEmpty().WithMessage("Subject name is required!")
                .NotNull().WithMessage("Subject name is required!")
                .MaximumLength(100).WithMessage("Subject name must less than 100 character!");
        }
    }
}
