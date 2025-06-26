using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.DTOs.Usuarios;

namespace WebMVC.Controllers
{
    public class UsuariosController: Controller
    {
        public string URLApi { get; set; }

        public UsuariosController(IConfiguration config)
        {
            URLApi = config.GetValue<string>("URLApiUsuarios");
        }

        //GET /api/Usuarios/login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }   
            try
            {
                var respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApi + "login", "post", dto, null);
                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    DatosUsuariosDTO us = JsonConvert.DeserializeObject<DatosUsuariosDTO>(body);
                    HttpContext.Session.SetString("LogToken", us.Token);
                    HttpContext.Session.SetString("LogEmail", us.Email);
                    HttpContext.Session.SetString("LogRol", us.Rol);
                    return RedirectToAction("Index", "Home");
                }
                else if ((int)respuesta.StatusCode == 403)
                {
                    ViewBag.Error = "Usted no esta autorizado para ingresar.";
                }
                else if((int)respuesta.StatusCode == 401)
                {
                    ViewBag.Error = "Usuario o contraseña incorrectos";
                }
                else
                {
                    ViewBag.Error = $"Error Api: {respuesta.StatusCode} - {body}";
                }
                //return View(dto);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error";
            }
            return View(dto);
        }

        //GET /api/Usuarios/ChngPass
        [HttpGet]
        public ActionResult ChngPass()
        {
            if (!(HttpContext.Session.GetString("LogRol") == "Cliente"))
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }
            string email = HttpContext.Session.GetString("LogEmail");
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "No se pudo obtener el email del usuario logueado.";
                return RedirectToAction("Login", "Usuarios");
            }
            var dto = new CambioContrasenaDTO
            {
                Email = email
            };
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChngPass(string passVieja, string passNueva, string passNuevaConfirmacion)
        {
            if (!(HttpContext.Session.GetString("LogRol") == "Cliente"))
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }
            string email = HttpContext.Session.GetString("LogEmail");
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "No se puede validar al usuario.";
                return RedirectToAction("Login", "Usuarios");
            }
            if (string.IsNullOrEmpty(passVieja) || string.IsNullOrEmpty(passNueva) || string.IsNullOrEmpty(passNuevaConfirmacion))
            {
                ViewBag.Error = "Los campos no pueden quedar vacios.";
                return View(new CambioContrasenaDTO { Email = email });
            }
            if (passNueva != passNuevaConfirmacion)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View(new CambioContrasenaDTO { Email = email });
            }
            try
            {
                var dto = new CambioContrasenaDTO
                {
                    Email = email,
                    PassVieja = passVieja,
                    PassNueva = passNueva
                };
                dto.Validar();

                var respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApi + "ChngPass", "put", dto, HttpContext.Session.GetString("LogToken"));
                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Contraseña cambiada correctamente.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = $"Error Api: {respuesta.StatusCode} - {body}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(new CambioContrasenaDTO { Email = email });
        }

        public ActionResult Logout()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
