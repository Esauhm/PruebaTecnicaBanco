using Microsoft.AspNetCore.Mvc;
using PruebaBanco.WebApp.Models;
using PruebaBanco.WebApp.Services.MovimientosTarjeta;
using PruebaBanco.WebApp.Services.Tarjetas;
using PruebaBanco.WebApp.ViewModels;

namespace PruebaBanco.WebApp.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly ITransaccionesService _transaccionesService;
        private readonly ITarjetaService _tarjetaService;

        public TransaccionesController(ITransaccionesService transaccionesService, ITarjetaService tarjetaService)
        {
            _transaccionesService = transaccionesService;
            _tarjetaService = tarjetaService;
        }
        public async Task<IActionResult> Index(int idTarjeta)
        {

            var currentDate = DateTime.Now;
            int mes = currentDate.Month;
            int anio = currentDate.Year;

            var tarjeta = await _tarjetaService.GetTarjetaByIdAsync(idTarjeta);
            var transacciones = await _transaccionesService.GetTransactionesAsync(idTarjeta, mes, anio);

            var viewModel = new TransaccionesDataViewModel
            {
                TarjetasCredito = tarjeta,
                Transacciones = transacciones
            };

            return View(viewModel);
        }
    }
}
