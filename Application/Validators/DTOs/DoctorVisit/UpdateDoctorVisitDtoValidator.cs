using Application.DTOs.DoctorVisit;
using FluentValidation;

namespace Application.Validators.DTOs.DoctorVisit
{
    public class UpdateDoctorVisitDtoValidator : AbstractValidator<UpdateDoctorVisitDto>
    {
        public UpdateDoctorVisitDtoValidator()
        {
            Include(new IBaseDtoValidator());
            Include(new IDoctorVisitDtoValidator());
        }
    }
}
