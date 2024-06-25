
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaBanco.WebApi.Data;
using PruebaBanco.WebApi.Models.Movimiento;
using PruebaBanco.WebApi.Repository.Transacciones;
using PruebaBanco.WebApi.Validators.Transacciones;
using PruebaBanco.WebApi.ViewModels;

namespace PruebaBanco.WebApi.Managers.Transacciones
{
    public class TransaccionesManager : ITransaccionesManager
    {
        private readonly ITransaccionesRepository _transaccionRepository;
        private readonly IMapper _mapper;

        public TransaccionesManager(ITransaccionesRepository transaccionesRepository, IMapper mapper)
        {
            _transaccionRepository = transaccionesRepository;
            _mapper = mapper;
        }



        //Para tarer las transacciones
        public async Task<IEnumerable<TransaccionesViewModel>> GetTransactionsAsync(int idTarjeta, int Mes, int Anio)
        {
           

            var transactions = await _transaccionRepository.GetTransactionesAsync(idTarjeta, Mes, Anio);
            return _mapper.Map<IEnumerable<TransaccionesViewModel>>(transactions);
        }



        //Para Realizar transacciones
        public async Task RealizarTransaccionAsync(TransaccionesViewModel transacciones)
        {
            var transaccion = _mapper.Map<TransaccionesEntity>(transacciones);
            await _transaccionRepository.AddTransaccionAsync(transaccion);
        }
    }
}
