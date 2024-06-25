using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaBanco.WebApi.Data;
using PruebaBanco.WebApi.Models.Movimiento;

namespace PruebaBanco.WebApi.Repository.Transacciones
{
    public class TransaccionesRepository : ITransaccionesRepository
    {
        private readonly BancoDbContext _context;

        public TransaccionesRepository(BancoDbContext context)
        {
            _context = context;
        }
      

        //Para Guardar Transacciones
        public async Task AddTransaccionAsync(TransaccionesEntity transacciones)
        {
            try
            {
                _context.Transacciones.Add(transacciones);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (using your logging framework of choice)
                throw new Exception("Error adding transaction", ex);
            }
        }


        //Para Obtener la transacciones
        public async Task<IEnumerable<TransaccionesEntity>> GetTransactionesAsync(int idTarjeta, int Mes, int Anio)
        {
            try
            {
                var query = _context.Transacciones.AsQueryable();

                query = query.Where(t => t.IdTarjeta == idTarjeta &&
                                         t.FechaTransaccion.HasValue &&
                                         t.FechaTransaccion.Value.Month == Mes &&
                                         t.FechaTransaccion.Value.Year == Anio)
                             .OrderByDescending(t => t.FechaTransaccion.Value);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (using your logging framework of choice)
                throw new Exception("Error retrieving transactions", ex);
            }

        }

    }
}
