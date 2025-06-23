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

        [HttpGet("BuscarPorTracking/{tracking}")]
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

        [HttpGet("BuscarPorCliente")]
        //[Authorize(Roles ="Cliente")]
        public IActionResult GetByEmail([FromQuery]string Email)
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
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PermisosException ex)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error, intente nuevamente más tarde");
            }
        }

        [HttpGet("BuscarPorComentario")]
        //[Authorize(Roles ="Cliente")]
        public IActionResult GetByComentariol([FromQuery]FiltroComentarioDTO datos)
        {
            if (datos == null)
            {
                return BadRequest("El email no puede ser nulo o vacío");
            }
            
            try
            {
                datos.Validar();
                IEnumerable<EnvioLigthDTO> envios = CUObtenerEnvio.getEnviosByComentario(datos);
                if (envios == null || !envios.Any())
                {
                    return NotFound("No se encontraron envíos para el cliente con email: " + datos.Email + " y comentario " + datos.Comentario);
                }
                return Ok(envios);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PermisosException ex)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error, intente nuevamente más tarde");
            }
        }

        [HttpGet("BuscarPorID/{id}")]
        //[Authorize(Roles ="Cliente")]
        public IActionResult GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El id no puede ser menor o igual a cero");
            }
            try
            {
                EnvioDTO envio = CUObtenerEnvio.getByID(id);
                if (envio == null)
                {
                    return NotFound("El envío con id=" + id + " no existe");
                }
                return Ok(envio);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PermisosException ex)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error, intente nuevamente más tarde");
            }
        }
    }
}
