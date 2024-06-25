using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using PruebaBanco.WebApp.Models;
using PruebaBanco.WebApp.Services.MovimientosTarjeta;
using PruebaBanco.WebApp.Services.Tarjetas;
using PruebaBanco.WebApp.ViewModels;
using System.Drawing;

namespace PruebaBanco.WebApp.Controllers
{
    public class TarjetaController : Controller
    {
        private readonly ITarjetaService _tarjetaService;
        private readonly ITransaccionesService _transaccionesService;

        public TarjetaController(ITarjetaService tarjetaService, ITransaccionesService transaccionesService)
        {
            _tarjetaService = tarjetaService;
            _transaccionesService = transaccionesService;
        }

        public async Task<IActionResult> Index(string numeroTarjeta)
        {
            var tarjeta = await _tarjetaService.GetByNumeroTarjeta(numeroTarjeta);

            if (tarjeta == null)
            {
                return NotFound();
            }

            var currentDate = DateTime.Now;
            int mes = currentDate.Month;
            int anio = currentDate.Year;

            var transacciones = await _transaccionesService.GetTransactionesAsync(tarjeta.id, mes, anio);

            var viewModel = new DataViewModel
            {
                Tarjeta = tarjeta,
                Transacciones = transacciones
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Transacciones([FromBody] TransaccionesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tarjetaService.RealizarTransaccion(model);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            return Json(new { success = false, message = "Datos inválidos" });
        }



        [HttpGet]
        public async Task<IActionResult> ExportarComprasAExcel(int idTarjeta)
        {

            var currentDate = DateTime.Now;
            int mes = currentDate.Month;
            int anio = currentDate.Year;

            var transacciones = await _transaccionesService.GetTransactionesAsync(idTarjeta, mes, anio); // Obtén las transacciones

            // Configurar el contexto de licencia de EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Compras");

                // Encabezados
                worksheet.Cells[1, 1].Value = "Fecha de Transacción";
                worksheet.Cells[1, 2].Value = "Descripción";
                worksheet.Cells[1, 3].Value = "Monto";

                // Aplicar estilo a los encabezados
                using (var range = worksheet.Cells[1, 1, 1, 3])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Datos
                int row = 2;
                foreach (var transaccion in transacciones.Where(t => t.TipoTransaccion != "1"))
                {
                    worksheet.Cells[row, 1].Value = transaccion.FechaTransaccion.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[row, 2].Value = transaccion.Descripcion;
                    worksheet.Cells[row, 3].Value = transaccion.Monto;
                    row++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                string excelName = $"Compras_Tarjeta_{idTarjeta}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }








    }
}
