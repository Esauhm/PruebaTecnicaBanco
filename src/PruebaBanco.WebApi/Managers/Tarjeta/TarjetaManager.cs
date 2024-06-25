using AutoMapper;
using PruebaBanco.WebApi.Models.Movimiento;
using PruebaBanco.WebApi.Models.Tarjetas;
using PruebaBanco.WebApi.Repository.Tarjeta;
using PruebaBanco.WebApi.Repository.Transacciones;
using PruebaBanco.WebApi.ViewModels;

namespace PruebaBanco.WebApi.Managers.Tarjeta
{
    public class TarjetaManager : ITarjetaManager
    {
        private readonly ITarjetaRepository _tarjetaRepository;
        private readonly IMapper _mapper;

        public TarjetaManager(ITarjetaRepository tarjetaRepository, IMapper mapper)
        {
            _tarjetaRepository = tarjetaRepository;
            _mapper = mapper;
        }

        //Para traer todas la tarjetas
        public async Task<List<TarjetaViewModel>> GetAll()
        {
            var tarjetas = await _tarjetaRepository.GetAll();
            return _mapper.Map<List<TarjetaViewModel>>(tarjetas);
        }


        //Para el estado de cuenta
        public async Task<TarjetaInfoViewModel> GetByNumeroTarjeta(string numeroTarjeta)
        {
            var tarjetaEntity = await _tarjetaRepository.GetByNumeroTarjeta(numeroTarjeta);
            return _mapper.Map<TarjetaInfoViewModel>(tarjetaEntity);
        }


        //Para los encabezados segun la tarjeta seleccionada
        async Task<PresentacionViewModel> ITarjetaManager.GetTarjetaByIdAsync(int IdTarjeta)
        {
            var tarjetaEntity = await _tarjetaRepository.GetTarjetaByIdAsync(IdTarjeta);
            return _mapper.Map<PresentacionViewModel>(tarjetaEntity);
        }
    }
}