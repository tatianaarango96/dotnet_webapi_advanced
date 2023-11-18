using FluentValidation;
using MyVaccine.WebApi.Dtos.Vaccine;

namespace MyVaccine.WebApi.Configurations.Validators
{
    public class VaccineDtoValidator : AbstractValidator<VaccineRequestDto>
    {
        public VaccineDtoValidator()
        {


            RuleFor(dto => dto.Name).NotEmpty().MaximumLength(255);

            RuleFor(dto => dto.RequiresBooster).NotNull().WithMessage("RequiresBooster no puede ser nulo");
            // Puedes agregar más reglas de validación condicionales aquí según sea necesario para el refuerzo.


        }
    }
}