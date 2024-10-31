using DogAPI.Common.DTOs;
using FluentValidation;

namespace DogAPI.Validator
{
    public class UpdateDogDTOValidator : AbstractValidator<UpdateDogRequestDTO>
    {
        public UpdateDogDTOValidator()
        {
            RuleFor(dto => dto.TailLenght)
                .GreaterThan(0)
                .WithMessage("Minimum number of tail lenght must be greater than 0");

            RuleFor(dto => dto.Weight)
                .GreaterThan(0)
                .WithMessage("Minimum number of weight must be greater than 0");
        }
    }
}
