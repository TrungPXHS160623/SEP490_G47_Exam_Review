using FluentValidation;
using Library.Request;
using Library.Response;

namespace WebClient.Validation
{
    public class ExamCreateRequestValidation : AbstractValidator<ExamCreateRequest>
    {
        public ExamCreateRequestValidation()
        {
            this.RuleFor(v => v.ExamCode)
                .NotEmpty().WithMessage("Exam Code is required!")
                .NotNull().WithMessage("Exam Code is required!")
                .MaximumLength(100).WithMessage("Exam Code must less than 100 character!");

            this.RuleFor(v => v.SubjectId)
                .NotEmpty().WithMessage("Select subject for exam!")
                .NotNull().WithMessage("Select subject for exam!");

            this.RuleFor(v => v.ExamDuration)
                .NotEmpty().WithMessage("Select exam duration!")
                .NotNull().WithMessage("Select exam duration!");

            this.RuleFor(v => v.TermDuration)
                .NotEmpty().WithMessage("Select exam term!")
                .NotNull().WithMessage("Select exam term!");

            this.RuleFor(v => v.ExamType)
                .NotEmpty().WithMessage("Select exam type!")
                .NotNull().WithMessage("Select exam type!");

            this.RuleFor(v => v.StartDate)
                .NotEmpty().WithMessage("Select start date to review!")
                .NotNull().WithMessage("Select start date to review!");

            this.RuleFor(v => v.EndDate)
                .NotEmpty().WithMessage("Select end date to review!")
                .NotNull().WithMessage("Select end date to review!");
        }
    }
}
