using Application.DTOs.SleepRecord;
using FluentValidation;

namespace Application.Validators.DTOs.SleepRecord
{
    public class ISleepRecordDtoValidator : AbstractValidator<ISleepRecordDto>
    {
        public ISleepRecordDtoValidator()
        {
            RuleFor(p => p.StartDateTime)
                .NotEmpty();

            RuleFor(p=>p.EndDateTime)
                .GreaterThan(p=>p.StartDateTime)
                .WithMessage("End date and time cannot be lower than start date and time");

            RuleFor(p => p.Notes)
                .MaximumLength(200);
        }
    }
}
