using CombinationGeneratorAPI.Application.DTOs;
using FluentValidation;
namespace CombinationGeneratorAPI.Application.Validators;
public class CombinationRequestValidator : AbstractValidator<CombinationRequest>
{
    public CombinationRequestValidator()
    {
        RuleFor(x => x.N).InclusiveBetween(1, 20).WithMessage("N must be between 1 and 20.");
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0).WithMessage("PageIndex must be 0 or greater.");
        RuleFor(x => x.PageSize).InclusiveBetween(1, 10).WithMessage("PageSize must be between 1 and 10.");
    }
}
