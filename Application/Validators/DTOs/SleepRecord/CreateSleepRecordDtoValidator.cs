using Application.DTOs.SleepRecord;
using FluentValidation;

namespace Application.Validators.DTOs.SleepRecord
{
    public class CreateSleepRecordDtoValidator : AbstractValidator<CreateSleepRecordDto>
    {
        public CreateSleepRecordDtoValidator()
        {
            Include(new ISleepRecordDtoValidator());
        }
    }
}
