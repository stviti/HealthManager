using Application.DTOs.SleepRecord;
using FluentValidation;

namespace Application.Validators.DTOs.SleepRecord
{
    public class UpdateSleepRecordDtoValidator : AbstractValidator<UpdateSleepRecordDto>
    {
        public UpdateSleepRecordDtoValidator()
        {
            Include(new IBaseDtoValidator());
            Include(new ISleepRecordDtoValidator());
        }
    }
}
