using Application.DTOs.Treatment;
using FluentValidation;

namespace Application.Validators.DTOs.Treatment
{
    public class CreateTreatmentDtoValidator : AbstractValidator<CreateTreatmentDto>
    {
        public CreateTreatmentDtoValidator()
        {
            Include(new ITreatmentDtoValidator());
        }
    }
}
