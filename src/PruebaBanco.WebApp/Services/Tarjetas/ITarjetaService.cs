using PruebaBanco.WebApp.Models;
using PruebaBanco.WebApp.ViewModels;

namespace PruebaBanco.WebApp.Services.Tarjetas
{
    public interface ITarjetaService
    {
        Task<List<TarjetaEntity>> GetAll();

        Task<TarjetaViewModel> GetByNumeroTarjeta(string numeroTarjeta);

        Task RealizarTransaccion(TransaccionesViewModel model);

        Task<PresentacionViewModel> GetTarjetaByIdAsync(int idTarjeta);

    }
}
