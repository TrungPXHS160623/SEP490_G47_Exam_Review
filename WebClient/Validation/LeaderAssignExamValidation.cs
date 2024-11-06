using FluentValidation;
using Library.Request;
using Library.Response;

namespace WebClient.Validation
{
    public class LeaderAssignExamValidation : AbstractValidator<LeaderExamResponse>
    {
        public LeaderAssignExamValidation()
        {
            this.RuleFor(v => v.AssignedLectureId)
                .NotEmpty().WithMessage("Please choose a lecturer to review this exam!")
                .NotNull().WithMessage("Please choose a lecturer to review this exam!");
        }
    }
}
