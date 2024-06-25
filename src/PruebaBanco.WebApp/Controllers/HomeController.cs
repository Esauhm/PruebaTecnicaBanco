using Microsoft.AspNetCore.Mvc;
using PruebaBanco.WebApp.Models;
using PruebaBanco.WebApp.Services.Tarjetas;
using PruebaBanco.WebApp.ViewModels;
using System.Diagnostics;

namespace PruebaBanco.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITarjetaService _tarjetaService;

        public HomeController(ILogger<HomeController> logger, ITarjetaService tarjetaService)
        {
            _logger = logger;
            _tarjetaService = tarjetaService;
        }

        public async Task<IActionResult> Index()
        {
            List<TarjetaEntity> tarjetasCredito = await _tarjetaService.GetAll();

            var tarjetasDetalles = new List<TarjetaViewModel>();
            foreach (var tarjeta in tarjetasCredito)
            {
                var tarjetaDetalle = await _tarjetaService.GetByNumeroTarjeta(tarjeta.numeroTarjeta);
                if (tarjetaDetalle != null)
                {
                    tarjetasDetalles.Add(tarjetaDetalle);
                }
            }

            var viewModel = new HomeViewModel
            {
                TarjetasCredito = tarjetasCredito,
                TarjetasDetalles = tarjetasDetalles
            };

            return View(viewModel);

        }


        //Tarea para traer las Tarjetas
        public async Task<IActionResult> GetTarjetas()
        {
            List<TarjetaEntity> TarjetasCredito = await _tarjetaService.GetAll();
            return View(TarjetasCredito);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
