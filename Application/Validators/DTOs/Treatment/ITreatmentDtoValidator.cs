using System;
using Application.DTOs.Treatment;
using FluentValidation;

namespace Application.Validators.DTOs.Treatment
{
    public class ITreatmentDtoValidator : AbstractValidator<ITreatmentDto>
    {
        public ITreatmentDtoValidator()
        {

            RuleFor(p => p.Name)
                .NotEmpty();

            RuleFor(p => p.StartDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today);

            RuleFor(p => p.RepeatOffset)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.RepeatOccurencies)
                .GreaterThanOrEqualTo(1)
                .LessThan(100);

            RuleFor(p=>p.Notes)
                .MaximumLength(200);

        }
    }
}
