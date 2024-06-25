using PruebaBanco.WebApi.Models.Movimiento;

namespace PruebaBanco.WebApi.Repository.Transacciones
{
    public interface ITransaccionesRepository
    {
        Task AddTransaccionAsync(TransaccionesEntity transacciones);

        Task<IEnumerable<TransaccionesEntity>> GetTransactionesAsync(int idTarjeta, int Mes, int Anio);
    }
}
