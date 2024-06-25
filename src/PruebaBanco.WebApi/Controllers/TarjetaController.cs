using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaBanco.WebApi.Managers.Tarjeta;
using PruebaBanco.WebApi.ViewModels;
using System.Transactions;

namespace PruebaBanco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly ITarjetaManager _manager;

        public TarjetaController(ITarjetaManager manager)
        {
            _manager = manager;
        }

        [HttpGet("AllTarjetas")]
        public async Task<IActionResult> GetTarjetas()
        {
            try
            {
                var tarjetas = await _manager.GetAll();
                return Ok(tarjetas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Error retrieving tarjetas.", error = ex.Message });
            }
        }


        [HttpPost("TarjetasByNumero")]
        public async Task<ActionResult<TarjetaInfoViewModel>> GetByNumeroTarjeta(string numeroTarjeta)
        {
            if (string.IsNullOrEmpty(numeroTarjeta))
            {
                return BadRequest(new { success = false, message = "Número de tarjeta es requerido." });
            }

            try
            {
                var tarjeta = await _manager.GetByNumeroTarjeta(numeroTarjeta);
                if (tarjeta == null)
                {
                    return NotFound(new { success = false, message = "Tarjeta no encontrada." });
                }
                return Ok(tarjeta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Error retrieving tarjeta.", error = ex.Message });
            }
        }


        [HttpGet("GetDataById")]
        public async Task<IActionResult> GetTarjetaById(int idTarjeta)
        {
            if (idTarjeta <= 0)
            {
                return BadRequest(new { success = false, message = "Id de tarjeta inválido." });
            }

            try
            {
                var tarjeta = await _manager.GetTarjetaByIdAsync(idTarjeta);
                if (tarjeta == null)
                {
                    return NotFound(new { success = false, message = "Tarjeta no encontrada." });
                }
                return Ok(tarjeta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Error retrieving tarjeta.", error = ex.Message });
            }
        }
    }
}
