using API.JWT;
using CasosDeUso.DTOs.Envio;
using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        public ILogin CULogin { get; set; }
        public ICambiarContraseña CuCambioContrasena { get; set; }

        public UsuariosController(ILogin cULogin, ICambiarContraseña cuCambioContrasena)
        {
            CULogin = cULogin;
            CuCambioContrasena = cuCambioContrasena;
        }

        /// <summary>
        /// Permite iniciar sesión.
        /// </summary>
        /// <param name="datos">Email y Password del cliente que busca iniciar sesión.</param>
        /// <returns>Datos del cliente que busca iniciar sesión o código de error</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(String), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(String), StatusCodes.Status500InternalServerError)]
        public ActionResult Login([FromBody] LoginDTO datos)
        {
            if (datos == null)
            {
                return BadRequest("Error datos login.");
            }
            try
            {
                datos.Validar();
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            try
            {
                UsuarioDTO user = CULogin.RealizarLogin(datos);
                if (user == null) {
                    return Unauthorized("Credenciales inválidas");
                }
                string token = ManejadorJWT.GenerarToken(user);

                var contenido = new { Email = user.Email, Rol = user.Rol, Token = token };

                return Ok(contenido);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(PermisosException ex)
            {
                return Forbid();
            }      
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ha ocurrido un error inesperado.");
            }
        }

        /// <summary>
        /// Permite cambiar contraseña del usuario dado
        /// </summary>
        /// <param name="datos">Email y Password vieja y nueva acambiar.</param>
        /// <returns>Ok o error.</returns>
        [HttpPut("ChngPass")]
        [Authorize(Roles = "Cliente")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(String), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(String), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(String), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(String), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(String), StatusCodes.Status500InternalServerError)]
        public ActionResult CambiarContrasena([FromBody]CambioContrasenaDTO datos)
        {
            string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if(email != datos.Email)
            {
                return BadRequest("El email del token no coincide con el email del usuario.");
            }


            if (datos == null)
            {
                return BadRequest("Error datos cambio contraseña.");
            }
            try
            {
                datos.Validar();
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            try
            {
                CuCambioContrasena.CambiarContraseña(datos);
                return Ok("Contraseña cambiada exitosamente.");
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error inesperado.");
            }


        }


    }
}
