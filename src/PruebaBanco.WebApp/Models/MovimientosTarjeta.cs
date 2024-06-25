namespace PruebaBanco.WebApp.Models
{
    public class MovimientosTarjeta
    {
        public int id { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaMovimiento { get; set; }

        public double? Monto { get; set; }

        public string? TipoMovimiento { get; set; }

        public int IdTarjeta { get; set; }

    }
}
