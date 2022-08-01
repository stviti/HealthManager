using System;
using Application.DTOs.DoctorVisit;
using FluentValidation;

namespace Application.Validators.DTOs.DoctorVisit
{
    public class IDoctorVisitDtoValidator : AbstractValidator<IDoctorVisitDto>
    {
        public IDoctorVisitDtoValidator()
        {
            RuleFor(p => p.DateTime)
                .GreaterThanOrEqualTo(DateTime.Today);

            RuleFor(p => p.Address)
                .NotEmpty();

            RuleFor(p => p.Notes)
                .MaximumLength(200);
        }
    }
}
