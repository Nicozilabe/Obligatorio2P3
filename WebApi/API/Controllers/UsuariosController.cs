using API.JWT;
using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        public ILogin CULogin { get; set; }

        public UsuariosController(ILogin cULogin)
        {
            CULogin = cULogin;
        }


        [HttpPost("login")]
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

                var contenido = new { user.Email, Rol = user.Rol, Token = token };

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
    }
}
