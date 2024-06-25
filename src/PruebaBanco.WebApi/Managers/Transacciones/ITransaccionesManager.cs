using PruebaBanco.WebApi.ViewModels;

namespace PruebaBanco.WebApi.Managers.Transacciones
{
    public interface ITransaccionesManager
    {
        Task RealizarTransaccionAsync(TransaccionesViewModel transacciones);

        Task<IEnumerable<TransaccionesViewModel>> GetTransactionsAsync(int idTarjeta, int Mes, int Anio);
    }
}
