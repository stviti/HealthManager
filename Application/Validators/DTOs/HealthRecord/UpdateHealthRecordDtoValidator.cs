using Application.DTOs.HealthRecord;
using FluentValidation;

namespace Application.Validators.DTOs.HealthRecord
{
    public class UpdateHealthRecordDtoValidator : AbstractValidator<UpdateHealthRecordDto>
    {
        public UpdateHealthRecordDtoValidator()
        {
            Include(new IBaseDtoValidator());
            Include(new IHealthRecordDtoValidator());
        }
    }
}
