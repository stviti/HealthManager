using Application.DTOs.HealthRecord;
using FluentValidation;

namespace Application.Validators.DTOs.HealthRecord
{
    public class CreateHealthRecordDtoValidator : AbstractValidator<CreateHealthRecordDto>
    {
        public CreateHealthRecordDtoValidator()
        {
            Include(new IHealthRecordDtoValidator());
        }
    }
}
