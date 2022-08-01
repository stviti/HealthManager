using Application.DTOs.HealthRecord;
using Application.Validators.DTOs.Medication;
using Application.Validators.DTOs.Symptom;
using FluentValidation;

namespace Application.Validators.DTOs.HealthRecord
{
    public class IHealthRecordDtoValidator : AbstractValidator<IHealthRecordDto>
    {
        public IHealthRecordDtoValidator()
        {
            RuleFor(p => p.StartDateTime)
                .NotEmpty();

            RuleFor(p => p.EndDateTime)
                .NotEmpty()
                .GreaterThan(p => p.StartDateTime)
                .WithMessage("End date and time cannot be lower than start date and time");

            RuleFor(p => p.Notes)
                .MaximumLength(200);

            RuleForEach(p => p.Symptoms)
                .SetValidator(new SymptomDtoValidator());

            RuleForEach(p => p.Medications)
                .SetValidator(new MedicationDtoValidator());

        }
    }
}
