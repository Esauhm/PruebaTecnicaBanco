using FluentValidation;
using PruebaBanco.WebApi.ViewModels;

namespace PruebaBanco.WebApi.Validators.Transacciones
{
    public class GetTransaccionesValidator : AbstractValidator<TransaccionesCamposViewModel>
    {

        public GetTransaccionesValidator()
        {
            RuleFor(x => x.IdTarjeta)
             .GreaterThan(0)
             .WithMessage("El Id de la tarjeta debe ser mayor que cero.")
             .NotNull();

            RuleFor(x => x.Mes)
                 .NotEmpty()
                 .WithMessage("El Mes de transacción es requerida.")
                 .NotNull();

            RuleFor(x => x.Anio)
                .NotEmpty()
                .WithMessage("El año de transacción es requerida.")
                .NotNull();
        }
    }
}
