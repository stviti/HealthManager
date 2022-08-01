using Application.DTOs;
using FluentValidation;

namespace Application.Validators.DTOs
{
    public class IBaseDtoValidator : AbstractValidator<IBaseDto>
    {
        public IBaseDtoValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();
        }
    }
}
