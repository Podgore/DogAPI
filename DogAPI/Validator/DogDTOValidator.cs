using DogAPI.Common.DTOs;
using FluentValidation;

namespace DogAPI.Validator
{
    public class DogDTOValidator : AbstractValidator<DogDTO>
    {
        public DogDTOValidator()
        {
            RuleFor(dto => dto.Name)
                .NotNull()
                .WithMessage("Name must be required");

            RuleFor(dto => dto.Color)
                .NotNull()
                .WithMessage("Color must be required");

            RuleFor(dto => dto.TailLenght)
                .NotNull()
                .WithMessage("Tail lenght must be required")
                .GreaterThan(0)
                .WithMessage("Minimum number of tail lenght must be greater than 0");

            RuleFor(dto => dto.Weight)
                .NotNull()
                .WithMessage("Weight must be required")
                .GreaterThan(0)
                .WithMessage("Minimum number of weight must be greater than 0");

        }
    }
}
