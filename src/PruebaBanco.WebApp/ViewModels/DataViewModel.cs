namespace PruebaBanco.WebApp.ViewModels
{
    public class DataViewModel
    {
        public TarjetaViewModel Tarjeta { get; set; }
        public IEnumerable<TransaccionesViewModel> Transacciones { get; set; }
    }
}
