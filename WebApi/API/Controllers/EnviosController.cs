using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        IObtenerEnvio CUObtenerEnvio { get; set; }
        IObtenerEnvioByTracking CUObtenerEnvioByTracking { get; set; }
        IObtenerEnvioByComentario CUObtenerEnvioByComentario { get; set; }
        IObtenerEnviosByEmail CUObtenerEnviosByEmail { get; set; }
        IObtenerEnviosByFecha CUObtenerEnviosByFecha { get; set; }

        public EnviosController(IObtenerEnvio cuObtenerEnvio, IObtenerEnvioByTracking cUObtenerEnvioByTracking, IObtenerEnvioByComentario cUObtenerEnvioByComentario, IObtenerEnviosByEmail cUObtenerEnviosByEmail, IObtenerEnviosByFecha cUObtenerEnviosByFecha)
        {
            CUObtenerEnvio = cuObtenerEnvio;
            CUObtenerEnvioByTracking = cUObtenerEnvioByTracking;
            CUObtenerEnvioByComentario = cUObtenerEnvioByComentario;
            CUObtenerEnviosByEmail = cUObtenerEnviosByEmail;
            CUObtenerEnviosByFecha = cUObtenerEnviosByFecha;
        }

        /// <summary>
        /// Permite buscar envíos por su código de tracking.
        /// </summary>
        /// <param name="tracking">Tracking del envío a buscar.</param>
        /// <returns>El Envío solicitado o mensaje de error</returns>
        [HttpGet("BuscarPorTracking/{tracking}")]
        [ProducesResponseType(typeof(EnvioDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(String), StatusCodes.Status500InternalServerError)]

        public IActionResult Get(int tracking)
        {
            if (tracking <= 0)
            {
                return BadRequest("El tracking no puede ser menor o igual a cero");
            }
            try
            {
                EnvioDTO e = CUObtenerEnvioByTracking.getByTracking(tracking);
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

        /// <summary>
        /// Permite buscar envíos por el Email de un Cliente.
        /// </summary>
        /// <param name="Email">Email del cliente al cual buscar sus envios.</param>
        /// <returns>Lista de los envíos del cliente con email dado</returns>
        [HttpGet("BuscarPorCliente")]
        [Authorize(Roles = "Cliente")]
        [ProducesResponseType(typeof(IEnumerable<EnvioLigthDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(String), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(String), StatusCodes.Status500InternalServerError)]
        public IActionResult GetByEmail([FromQuery]string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return BadRequest("El email no puede ser nulo o vacío");
            }
            try
            {
                IEnumerable<EnvioLigthDTO> envios = CUObtenerEnviosByEmail.getEnviosByEmail(Email);
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

        /// <summary>
        /// Permite buscar envíos con el comentario y email dado.
        /// </summary>
        /// <param name="datos">Email y Comentario por el cual buscar envíos.</param>
        /// <returns>Los Envíos buscados o código de error.</returns>
        [HttpGet("BuscarPorComentario")]
        [Authorize(Roles = "Cliente")]
        [ProducesResponseType(typeof(IEnumerable<EnvioLigthDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(String), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(String), StatusCodes.Status500InternalServerError)]
        public IActionResult GetByComentariol([FromQuery]FiltroComentarioDTO datos)
        {
            if (datos == null)
            {
                return BadRequest("El email no puede ser nulo o vacío");
            }
            
            try
            {
                datos.Validar();
                IEnumerable<EnvioLigthDTO> envios = CUObtenerEnvioByComentario.getEnviosByComentario(datos);
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

        /// <summary>
        /// Permite buscar envíos por su código Id.
        /// </summary>
        /// <param name="id">Id del envío a buscar.</param>
        /// <returns>El tema solicitado o mensaje de error</returns>
        [HttpGet("BuscarPorID/{id}")]
        [Authorize(Roles = "Cliente")]
        [ProducesResponseType(typeof(EnvioDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(String), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(String), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Permite buscar envíos por fecha y estado.
        /// </summary>
        /// <param name="filtro">Fecha de inicio, fecha fin y estado de los envíos a buscar.</param>
        /// <returns>El tema solicitado o mensaje de error</returns>
        [HttpGet("BuscarPorFecha")]
        [Authorize(Roles = "Cliente")]
        [ProducesResponseType(typeof(IEnumerable<EnvioLigthDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(String), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(String), StatusCodes.Status500InternalServerError)]
        public IActionResult GetByFecha([FromQuery] FiltroFechaDTO filtro)
        {
            if (filtro == null)
            {
                return BadRequest("El filtro no puede ser nulo");
            }

            try
            {
                filtro.Validar();
                IEnumerable<EnvioLigthDTO> envios = CUObtenerEnviosByFecha.getEnviosByFecha(filtro);
                if (envios == null || !envios.Any())
                {
                    return NotFound("No se encontraron envíos para el filtro proporcionado");
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
    }
}