using PruebaBanco.WebApi.Models.Tarjetas;
using PruebaBanco.WebApi.Repository.Tarjeta;
using PruebaBanco.WebApi.ViewModels;

namespace PruebaBanco.WebApi.Managers.Tarjeta
{
    public interface ITarjetaManager
    {

        public Task<List<TarjetaViewModel>> GetAll();

        Task<TarjetaInfoViewModel> GetByNumeroTarjeta(string numeroTarjeta);

        public Task<PresentacionViewModel> GetTarjetaByIdAsync(int IdTarjeta);
    }
}
