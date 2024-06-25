using PruebaBanco.WebApp.Models;

namespace PruebaBanco.WebApp.ViewModels
{
    public class TransaccionesDataViewModel
    {
        public PresentacionViewModel TarjetasCredito { get; set; }
        public IEnumerable<TransaccionesViewModel> Transacciones { get; set; }
    }
}
