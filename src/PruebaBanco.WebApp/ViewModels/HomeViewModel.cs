using PruebaBanco.WebApp.Models;

namespace PruebaBanco.WebApp.ViewModels
{
    public class HomeViewModel
    {
        public List<TarjetaEntity> TarjetasCredito { get; set; }
        public List<TarjetaViewModel> TarjetasDetalles { get; set; }
    }
}
