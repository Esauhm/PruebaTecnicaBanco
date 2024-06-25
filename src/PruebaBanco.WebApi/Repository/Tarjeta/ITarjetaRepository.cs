using PruebaBanco.WebApi.Models.Tarjetas;
using PruebaBanco.WebApi.ViewModels;

namespace PruebaBanco.WebApi.Repository.Tarjeta
{
    public interface ITarjetaRepository
    {
        Task<List<TarjetasEntity>> GetAll();

        Task<TarjetaInfoViewModel> GetByNumeroTarjeta(string numeroTarjeta);

        Task<TarjetasEntity> GetTarjetaByIdAsync(int IdTarjeta);

    }
}
