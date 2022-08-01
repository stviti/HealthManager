using Application.DTOs.Medication;
using FluentValidation;

namespace Application.Validators.DTOs.Medication
{
    public class MedicationDtoValidator : AbstractValidator<MedicationDto>
    {
        public MedicationDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(p => p.Dose)
                .MaximumLength(50);
        }
    }
}
