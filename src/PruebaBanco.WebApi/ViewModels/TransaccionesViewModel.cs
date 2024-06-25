namespace PruebaBanco.WebApi.ViewModels
{
    public class TransaccionesViewModel
    {
        public int IdTarjeta { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaTransaccion { get; set; }

        public double Monto { get; set; }

        public string? TipoTransaccion { get; set; }

    }
}
