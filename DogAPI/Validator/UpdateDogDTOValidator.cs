using DogAPI.Common.DTOs;
using FluentValidation;

namespace DogAPI.Validator
{
    public class UpdateDogDTOValidator : AbstractValidator<UpdateDogDTO>
    {
        public UpdateDogDTOValidator()
        {
            RuleFor(dto => dto.TailLenght)
                .GreaterThan(0)
                .WithMessage("Tail lenght must be greater than 0");

            RuleFor(dto => dto.Weight)
                .GreaterThan(0)
                .WithMessage("Weight must be greater than 0");
        }
    }
}
