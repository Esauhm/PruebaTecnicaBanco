using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaBanco.WebApi.Managers.Transacciones;
using PruebaBanco.WebApi.Validators.Transacciones;
using PruebaBanco.WebApi.ViewModels;
using System.Transactions;

namespace PruebaBanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionesManager _transaccionesManager;

        public TransaccionesController(ITransaccionesManager transaccionesManager)
        {
            _transaccionesManager = transaccionesManager;
        }

        [HttpPost("RealizarTransaccion")]
        public async Task<IActionResult> RealizarTransacciones([FromBody] TransaccionesViewModel model)
        {
                try
                {
                    var validator = new TransaccionesValidator();
                    var result = validator.Validate(model);

                    if (!result.IsValid)
                    {
                         return BadRequest(new { success = false, message = result.Errors.ToList() });
                    }

                    await _transaccionesManager.RealizarTransaccionAsync(model);
                    return Ok(new { success = true });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { success = false, message = ex.Message });
                }
        }

        [HttpGet("GetTransacciones")]
        public async Task<IActionResult> GetTransactiones([FromQuery] TransaccionesCamposViewModel model)
        {
            try
            {
                var validator = new GetTransaccionesValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new { success = false, message = result.Errors.ToList() });
                }

                var transactions = await _transaccionesManager.GetTransactionsAsync(model.IdTarjeta, model.Mes, model.Anio);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Se produjo un error al obtener las transacciones." });
            }
        }
    }
}
