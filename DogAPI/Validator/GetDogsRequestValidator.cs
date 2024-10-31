using DogAPI.Common.DTOs;
using FluentValidation;

namespace DogAPI.Validator
{
    public class GetDogsRequestValidator : AbstractValidator<GetDogsRequest>
    {
        public GetDogsRequestValidator()
        {
            RuleFor(request => request.Page)
                .GreaterThan(0);

            RuleFor(request => request.PageSize)
                .GreaterThan(0);
        }
    }
}
