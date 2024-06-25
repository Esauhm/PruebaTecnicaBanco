namespace PruebaBanco.WebApp.ViewModels
{
    public class TransaccionesViewModel
    {
        public DateTime FechaTransaccion { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public int IdTarjeta { get; set; }
        public string? TipoTransaccion { get; set; }
    }
}
