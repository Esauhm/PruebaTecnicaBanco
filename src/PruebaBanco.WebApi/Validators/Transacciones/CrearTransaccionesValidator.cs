using FluentValidation;
using PruebaBanco.WebApi.ViewModels;

namespace PruebaBanco.WebApi.Validators.Transacciones
{
    public class CrearTransaccionesValidator : AbstractValidator<TransaccionesViewModel>
    {

        public CrearTransaccionesValidator()
        {
            RuleFor(x => x.IdTarjeta)
             .GreaterThan(0)
             .WithMessage("El Id de la tarjeta debe ser mayor que cero.")
             .NotNull();

            RuleFor(x => x.FechaTransaccion)
                .NotEmpty()
                .WithMessage("La fecha de transacción es requerida.")
                .NotNull();

            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage("La descripción es requerida.")
                .MaximumLength(100)
                .WithMessage("La descripción no puede tener más de 100 caracteres.")
                .NotNull();

            RuleFor(x => x.Monto)
                .GreaterThan(0)
                .WithMessage("El monto debe ser mayor que cero.")
                .NotNull();

            RuleFor(x => x.TipoTransaccion)
                .NotEmpty()
                .WithMessage("El tipo de transacción es requerido.")
                .Must(tipo => tipo == "0" || tipo == "1")
                .WithMessage("El tipo de transacción debe ser '0' (Cargo) o '1' (Abono).")
                .NotNull();

        }
    
    }
}
