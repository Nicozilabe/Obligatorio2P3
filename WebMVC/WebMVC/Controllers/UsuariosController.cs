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

        public ActionResult Logout()
        {
            return View();
        }
    }
}
