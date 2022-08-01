using System;
using Application.DTOs.Symptom;
using FluentValidation;

namespace Application.Validators.DTOs.Symptom
{
    public class SymptomDtoValidator : AbstractValidator<SymptomDto>
    {
        public SymptomDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(p => p.Description)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);
        }
    }
}
