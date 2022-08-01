using Application.DTOs.Treatment;
using FluentValidation;

namespace Application.Validators.DTOs.Treatment
{
    public class UpdateTreatmentDtoValidator : AbstractValidator<UpdateTreatmentDto>
    {
        public UpdateTreatmentDtoValidator()
        {
            Include(new IBaseDtoValidator());
            Include(new ITreatmentDtoValidator());
        }
    }
}
