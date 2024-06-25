using Microsoft.EntityFrameworkCore;
using PruebaBanco.WebApi.Models.Movimiento;
using PruebaBanco.WebApi.Models.Tarjetas;

namespace PruebaBanco.WebApi.Data
{
    public interface IBancoDbContext
    {
        DbSet<TarjetasEntity> Tarjetas { get; set; }
        DbSet<TransaccionesEntity> Transacciones { get; set; }
    }
}
