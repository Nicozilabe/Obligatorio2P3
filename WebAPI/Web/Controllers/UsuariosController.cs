using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using Humanizer;
using LogicaAplicacion.CasosUsoConcretos;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        public ILogin CULogin { get; set; }
        public IRegistroEmpleado CuRegistroEmpleado { get; set; }
        public IListarEmpleados CUListarEmpleados { get; set; }
        public IObtenerEmpleado CUObtenerEmpleado { get; set; }
        public IEditarEmpleado CUEditarEmpleado { get; set; }
        public IBajaEmpleado CUBajaEmpleado { get; set; }

        public UsuariosController(ILogin cULogin, IRegistroEmpleado cUregistroEmpleado, IListarEmpleados cUListarEmpleados, IObtenerEmpleado cUObtenerEmpleado, IEditarEmpleado cUEditarEmpleado, IBajaEmpleado cUBajaEmpleado)
        {
            CULogin = cULogin;
            CuRegistroEmpleado = cUregistroEmpleado;
            CUListarEmpleados = cUListarEmpleados;
            CUObtenerEmpleado = cUObtenerEmpleado;
            CUEditarEmpleado = cUEditarEmpleado;
            CUBajaEmpleado = cUBajaEmpleado;
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDTO datos)
        {
            try
            {
                UsuarioDTO user = CULogin.RealizarLogin(datos);
                HttpContext.Session.SetInt32("LogeadoId", user.Id);
                HttpContext.Session.SetString("LogeadoRol", user.Rol);
                ViewBag.ErrorMessage = (user.Nombre + user.Apellido + user.Email + user.Rol + user.Id);
                return RedirectToAction("Index", "Home");
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
                ViewBag.ErrorMessage = "Ocurrió un error inesperado al iniciar sesión.";
            }
            return View();
        }

        public ActionResult Registro()
        {
            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador")
            {
                ViewBag.IdAdmin = HttpContext.Session.GetInt32("LogeadoId");
                return View();
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }


        }

        [HttpPost]
        public ActionResult Registro(RegistroEmpleadoDTO datos)
        {
            ViewBag.IdAdmin = HttpContext.Session.GetInt32("LogeadoId");
            try
            {
                datos.Validar();
                UsuarioDTO creado = CuRegistroEmpleado.RegistrarEmpleado(datos);
                ViewBag.ErrorMessage = "Usuario creado exitosamente.";
            }catch (DatosInvalidosException ex)
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




        public ActionResult Empleados()
        {
            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador")
            {
                try
                {
                    return View(CUListarEmpleados.ListarTodosLosEmpleados());
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
                    ViewBag.ErrorMessage = "Ocurrió un error al listar los empleados.";
                }
                return View();  
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }

        }

        public ActionResult EditarEmpleado(int id)
        {

            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador")
            {

                EmpleadoDTO emp = null;
                try
                {
                    emp = CUObtenerEmpleado.FindById(id);
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

                return View(emp);


            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }

        }

        [HttpPost]
        public ActionResult EditarEmpleado(EmpleadoDTO dto)
        {
            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador")
            {
                try
                {
                    int? IdRealizador = HttpContext.Session.GetInt32("LogeadoId");
                    dto.Validar();
                    CUEditarEmpleado.EditarEmpleado(dto, IdRealizador);
                    ViewBag.ErrorMessage = "Empleado editado correctamente";
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

                return View(CUObtenerEmpleado.FindById(dto.Id));


            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }

        }

        public ActionResult BajaEmpleado(int id)
        {
  
            if (HttpContext.Session.GetString("LogeadoRol") == "Administrador")
            {
                EmpleadoDTO dto = null;
                try
                {
                     dto = CUObtenerEmpleado.FindById(id);
                    
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
                    ViewBag.ErrorMessage = "Ocurrió un error al recuoerar los datos del usuario";
                }
                return View(dto);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Auth");
            }



        }

        [HttpPost]
        public ActionResult BajaEmpleado(EmpleadoDTO dto, bool confirmacion)
        {
            if (!confirmacion)
            {

                ViewBag.ErrorMessage = "Debe confirmar la acción para que se efectue";
                return View(CUObtenerEmpleado.FindById(dto.Id));

            }
            else
            {

                if (HttpContext.Session.GetString("LogeadoRol") == "Administrador")
                {
                    int? IdRealizador = HttpContext.Session.GetInt32("LogeadoId");
                    try
                    {
                        CUBajaEmpleado.RelizarBaja(dto.Id, IdRealizador);
                        ViewBag.ErrorMessage = "Empleado dado de baja correctamente";
                    }
                    catch (DatosInvalidosException ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                    }
                    catch (PermisosException ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                    }catch(OperacionConflictivaExeption ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Ocurrió un error al eliminar el usuario.";
                    }
                    return View(dto);
                }
                else
                {
                    return RedirectToAction("NoAutorizado", "Auth");
                }
            }
        }

        public IActionResult Logout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Logout(int? logue)
        {
            if (HttpContext.Session.GetInt32("LogeadoId") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            HttpContext.Session.Clear();
            int? log = HttpContext.Session.GetInt32("LogeadoId");
            return RedirectToAction("Index", "Home");
        }
    }
}
