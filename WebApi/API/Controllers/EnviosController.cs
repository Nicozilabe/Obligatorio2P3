using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {

        IObtenerEnvio CUObtenerEnvio { get; set; }

        public EnviosController(IObtenerEnvio cuObtenerEnvio)
        {
            CUObtenerEnvio = cuObtenerEnvio;
        }

        [HttpGet("RutaBuscarPorTracking/{tracking}")]
        public IActionResult Get(int tracking)
        {
            if (tracking <= 0)
            {
                return BadRequest("El tracking no puede ser menor o igual a cero");
            }
            try
            {
                EnvioDTO e = CUObtenerEnvio.getByTracking(tracking);
                if (e == null)
                {
                    return NotFound("El envio con tracking=" + tracking + " no existe");
                }
                return Ok(e);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error, intente nuevamente más tarde");
            }
        }

        [HttpGet("RutaBuscarPorCliente")]
        //[Authorize(Roles ="Cliente")]
        public IActionResult GetByEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return BadRequest("El email no puede ser nulo o vacío");
            }
            try
            {
                IEnumerable<EnvioLigthDTO> envios = CUObtenerEnvio.getEnviosByEmail(Email);
                if (envios == null || !envios.Any())
                {
                    return NotFound("No se encontraron envíos para el cliente con email: " + Email);
                }
                return Ok(envios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error, intente nuevamente más tarde");
            }
        }
    }
}
