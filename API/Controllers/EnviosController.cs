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

        [HttpGet("{tracking}", Name = "RutaBuscarPorTracking")]
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
    }
}
