using Application.DTOs.DoctorVisit;
using FluentValidation;

namespace Application.Validators.DTOs.DoctorVisit
{
    public class CreateDoctorVisitDtoValidator : AbstractValidator<CreateDoctorVisitDto>
    {
        public CreateDoctorVisitDtoValidator()
        {
            Include(new IDoctorVisitDtoValidator());
        }
    }
}
