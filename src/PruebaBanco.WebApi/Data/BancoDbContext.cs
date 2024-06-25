using Microsoft.EntityFrameworkCore;
using PruebaBanco.WebApi.Models.Movimiento;
using PruebaBanco.WebApi.Models.Tarjetas;

namespace PruebaBanco.WebApi.Data
{
    public class BancoDbContext : DbContext , IBancoDbContext
    {
        public BancoDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TarjetasEntity> Tarjetas { get; set; }
        public DbSet<TransaccionesEntity> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación entre MovimientosTarjeta y Tarjetas
            modelBuilder.Entity<TransaccionesEntity>()
                        .HasOne(mt => mt.TarjetaNavegation)
                        .WithMany(t => t.MovimientosTarjeta)
                        .HasForeignKey(mt => mt.IdTarjeta);
        }

    }
}
