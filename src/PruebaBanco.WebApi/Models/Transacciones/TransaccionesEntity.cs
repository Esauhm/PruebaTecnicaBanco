using PruebaBanco.WebApi.Models.Tarjetas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBanco.WebApi.Models.Movimiento
{
    [Table("TransaccionesTarjeta")]
    public class TransaccionesEntity
    {
        [Key]
        public int id { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaTransaccion { get; set; }

        public double? Monto { get; set; }

        public string? TipoTransaccion { get; set; }

        public int IdTarjeta { get; set; }

        public virtual TarjetasEntity TarjetaNavegation { get; set; } = null!;
    }
}
