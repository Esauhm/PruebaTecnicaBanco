using AutoMapper;
using PruebaBanco.WebApi.Models.Movimiento;
using PruebaBanco.WebApi.Models.Tarjetas;
using PruebaBanco.WebApi.ViewModels;

namespace PruebaBanco.WebApi.Mapping
{
    public class MapperProfile : Profile
    {

        public MapperProfile() 
        {
            // Mapeos de TarjetasEntity a ViewModels
            CreateMap<TarjetasEntity, TarjetaViewModel>();
            CreateMap<TarjetasEntity, TarjetaInfoViewModel>();
            CreateMap<TarjetasEntity, PresentacionViewModel>();

            // Mapeos de TransaccionesEntity a ViewModels
            CreateMap<TransaccionesEntity, TransaccionesViewModel>();

            // Mapeo inverso si es necesario
            CreateMap<TransaccionesViewModel, TransaccionesEntity>();
        }
    }
}
