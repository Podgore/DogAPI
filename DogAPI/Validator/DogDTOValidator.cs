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
                .WithMessage("Name is required");

            RuleFor(dto => dto.Color)
                .NotNull()
                .WithMessage("Color is required");

            RuleFor(dto => dto.TailLenght)
                .NotNull()
                .WithMessage("Tail lenght is required")
                .GreaterThan(0)
                .WithMessage("Tail lenght must be greater than 0");

            RuleFor(dto => dto.Weight)
                .NotNull()
                .WithMessage("Weight must is required")
                .GreaterThan(0)
                .WithMessage("Weight must be greater than 0");

        }
    }
}
