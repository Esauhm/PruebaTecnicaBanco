using PruebaBanco.WebApi.Models.Movimiento;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBanco.WebApi.Models.Tarjetas
{
    [Table("Tarjetas")]
    public class TarjetasEntity
    {
        [Key]
        public int Id { get; set; }
        public string? NombreTitular { get; set; }

        public string? NumeroTarjeta { get; set; }

        public double? MontoOtorgado { get; set; }

        public double? PorcentajeInteres { get; set; }

        public double? PorcentajeInteresMinimo { get; set; }

        public virtual ICollection<TransaccionesEntity> MovimientosTarjeta { get; set; } = new List<TransaccionesEntity>();

    }
}
