using CasosDeUso.DTOs.Envio;
using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.CasosUsoConcretos.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Controllers
{
    public class EnviosController : Controller
    {
        public IObtenerAgencias CUObtenerAgencias { get; set; }
        public IObtenerCiudades CUObtenerCiudades { get; set; }
        public IAltaEnvio CUAltaEnvio { get; set; }
        public IObtenerEnvio CUObtenerEnvios { get; set; }
        public IFinalizarEnvio CUFinalizarEnvio { get; set; }
        public IComentarioEnvio CUComentarioEnvio { get; set; }




        public EnviosController(IObtenerAgencias cUObtenerAgencias, IObtenerCiudades cUObtenerCiudades, IAltaEnvio cUAltaEnvio, IObtenerEnvio cUObtenerEnvios, IFinalizarEnvio cUFinalizarEnvio, IComentarioEnvio cUComentarioEnvio)
        {
            CUObtenerAgencias = cUObtenerAgencias;
            CUObtenerCiudades = cUObtenerCiudades;
            CUAltaEnvio = cUAltaEnvio;
            CUObtenerEnvios = cUObtenerEnvios;
            CUFinalizarEnvio = cUFinalizarEnvio;
            CUComentarioEnvio = cUComentarioEnvio;
        }

        public IActionResult Activos()
        {

            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador" || HttpContext.Session.GetString("LogeadoRol") == "Empleado")
            {
                IEnumerable<EnvioLigthDTO> envios = null;
                try
                {
                    envios = CUObtenerEnvios.getEnviosLightActivos();
                }
                catch (DatosInvalidosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (PermisosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ha ocurrido un error inesperado";
                }
                return View(envios);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }

        }


        public IActionResult AltaEnvio()

        {
            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador" || HttpContext.Session.GetString("LogeadoRol") == "Empleado")
            {

                try
                {
                    IEnumerable<AgenciaDTO> agencias = CUObtenerAgencias.GetAgencias();
                    IEnumerable<CiudadDTO> ciudades = CUObtenerCiudades.GetCiudades();
                    RegistroEnvioViewModel model = new RegistroEnvioViewModel
                    {
                        direccion = new DireccionDTO(),
                        Agencias = agencias,
                        Ciudades = ciudades,
                        EmailCliente = "",
                        Peso = 0,
                        TipoEnvio = "",
                        IdAgencia = 0
                    };

                    return View(model);

                }
                catch (DatosInvalidosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (PermisosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ocurrió un error inesperado al registrar el usuario.";
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }
        }
        [HttpPost]
        public IActionResult AltaEnvio(RegistroEnvioViewModel model)
        {

            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador" || HttpContext.Session.GetString("LogeadoRol") == "Empleado")
            {

                IEnumerable<AgenciaDTO> agencias = CUObtenerAgencias.GetAgencias();
                IEnumerable<CiudadDTO> ciudades = CUObtenerCiudades.GetCiudades();

                RegistroEnvioViewModel model2 = new RegistroEnvioViewModel
                {
                    direccion = new DireccionDTO(),
                    Agencias = agencias,
                    Ciudades = ciudades,
                    EmailCliente = "",
                    Peso = 0,
                    TipoEnvio = "",
                    IdAgencia = 0
                };

                RegistroEnvioDTO reg = new RegistroEnvioDTO
                {
                    IdEmpleadoResponable = HttpContext.Session.GetInt32("LogeadoId"),
                    EmailCliente = model.EmailCliente,
                    Peso = model.Peso,
                    TipoEnvio = model.TipoEnvio,
                    IdAgencia = model.IdAgencia,
                    IdCiudad = model.IdCiudad,
                    direccion = model.direccion
                };
                try
                {
                    reg.Validar();
                    CUAltaEnvio.RegistroEnvio(reg);
                    ViewBag.ErrorMessage = "Envío registrado correctamente.";
                }
                catch (DatosInvalidosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (PermisosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ha ocurrido un error inesperado";
                }
                return View(model2);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }

            

        }
        public ActionResult FinalizarEnvio(int id)
            {
            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador" || HttpContext.Session.GetString("LogeadoRol") == "Empleado")
            {

                EnvioDTO env = null;
                try
                {
                    env = CUObtenerEnvios.getByID(id);
                }
                catch (DatosInvalidosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (PermisosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ocurrió un error inesperado al editar el usuario.";
                }

                return View(env);


            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }
        }
        [HttpPost]
        public ActionResult FinalizarEnvio(int Id, DateTime FechaEntrega)
        {

            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador" || HttpContext.Session.GetString("LogeadoRol") == "Empleado")
            {

                if (Id <= 0)
                {
                    ViewBag.ErrorMessage= "El id del envío no es válido";
                }
                if (FechaEntrega == null)
                {
                    ViewBag.ErrorMessage ="La fecha no es válida";
                }
                try
                {
                    CUFinalizarEnvio.finalizarEnvio(Id, FechaEntrega);
                    ViewBag.ErrorMessage = "Envio finalizado exitosamente.";
                }
                catch (DatosInvalidosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (PermisosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ocurrió un error inesperado al editar el usuario.";
                }
                return View(CUObtenerEnvios.getByID(Id));


            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }


        }

        public ActionResult AgregarComentario(int Id)
        {   
            EnvioDTO env = null;

            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador" || HttpContext.Session.GetString("LogeadoRol") == "Empleado")
            {
                
                try
                {
                    env = CUObtenerEnvios.getByID(Id);
                    ViewBag.Envio = env;
                }
                catch (DatosInvalidosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (PermisosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ocurrió un error inesperado al editar el usuario.";
                }

                


            }
            return View();
        }

        [HttpPost]
        public ActionResult AgregarComentario(ComentarioEnvioDTO datos , int EnvioID)
        {
            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador" || HttpContext.Session.GetString("LogeadoRol") == "Empleado")
            {
                datos.Validar();
                try
                {
                    datos.EmpleadoId = HttpContext.Session.GetInt32("LogeadoId");
                    CUComentarioEnvio.AgregarComentario(EnvioID, datos);
                    ViewBag.Envio = CUObtenerEnvios.getByID(EnvioID);
                    
                    ViewBag.ErrorMessage = "Comentario agregado exitosamente.";
                }
                catch (DatosInvalidosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (PermisosException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ocurrió un error inesperado al agregar el comentario.";
                }
            }
            return View();
        }
    }
}
