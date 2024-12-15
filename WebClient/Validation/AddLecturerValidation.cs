using FluentValidation;
using Library.Request;

namespace WebClient.Validation
{
    public class AddLecturerValidation : AbstractValidator<AddLecturerSubjectRequest>
    {
        public AddLecturerValidation() 
        {
            this.RuleFor(v => v.Mail)
                .NotEmpty().WithMessage("Fpt mail is required!")
                .NotNull().WithMessage("Fpt mail is required!")
                .MaximumLength(30).WithMessage("Fpt mail must less than 30 character!");

            this.RuleFor(v => v.FullName)
                .NotEmpty().WithMessage("Lecturer's name is required!")
                .NotNull().WithMessage("Lecturer's name is required!");

            this.RuleFor(v => v.MailFe)
                .NotEmpty().WithMessage("Fe mail is required!")
                .NotNull().WithMessage("Fe mail is required!")
                .MaximumLength(30).WithMessage("Fe mail must less than 30 character!");

            this.RuleFor(v => v.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required!")
                .NotNull().WithMessage("Phone number is required!")
                .Matches(@"^\d{10}$")
                .WithMessage("Invalid phone number format! The phone number must contain exactly 10 digits.");
        }
    }
}
