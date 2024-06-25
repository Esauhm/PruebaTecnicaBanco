using PruebaBanco.WebApp.ViewModels;

namespace PruebaBanco.WebApp.Services.MovimientosTarjeta
{
    public interface ITransaccionesService
    {
        Task<IEnumerable<TransaccionesViewModel>> GetTransactionesAsync(int cardId, int month, int year);

    }
}
