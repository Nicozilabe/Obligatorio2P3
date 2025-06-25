using WebMVC.DTOs.Envio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Runtime.Intrinsics.X86;
using Newtonsoft.Json;

namespace WebMVC.Controllers
{
    public class EnviosController : Controller
    {
        public string URLApi { get; set; }

        public EnviosController(IConfiguration config)
        {
            URLApi = config.GetValue<string>("UrlApiEnvios");
        }

        //GET envios
        //vista para ingresar el numero de tracking F1
        public ActionResult Seguimiento()
        {
            return View();
        }
        //POST envis
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Seguimiento(int tracking)
        {
            EnvioDTO dto = null;
            try
            {
                HttpResponseMessage resultado = AuxiliarClienteHttp.EnviarSolicitud(URLApi + "BuscarPorTracking/" + tracking, "get", null, null);
                string body = AuxiliarClienteHttp.ObtenerBody(resultado);

                if (resultado.IsSuccessStatusCode)
                {
                    dto = JsonConvert.DeserializeObject<EnvioDTO>(body);
                }
                else
                {
                    ViewBag.Error = body;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrio un error en la obtencion del envio";
            }
            return View("Detalles", dto);
        }
        //GET detalles
        public ActionResult Detalles(EnvioDTO dto)
        {
            return View(dto);
        }

        //GET envios listar mis ewnvios
        public ActionResult ListarMisEnvios()
        {
            List<EnvioLigthDTO> envios = new List<EnvioLigthDTO>();
            string email = HttpContext.Session.GetString("LogEmail");
            string token = HttpContext.Session.GetString("LogToken");
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Usuarios");
            }

            var url = $"{URLApi}BuscarPorCliente?Email={email}";
            var resp = AuxiliarClienteHttp.EnviarSolicitud(url, "get", null, token);
            string body = AuxiliarClienteHttp.ObtenerBody(resp);
            if (!resp.IsSuccessStatusCode)
            {
                ViewBag.Error = $"Error API: {resp.StatusCode} - {body}";
                return View(new List<EnvioLigthDTO>());
            }
            var lista = JsonConvert.DeserializeObject<List<EnvioLigthDTO>>(body);
            var ordenada = lista.OrderByDescending(e => e.FechaRegistroEnvio).ToList();

            return View("ListarMisEnvios", ordenada);
        }
        [HttpGet]
        public ActionResult DetallesEnvio(int id)
        {
            EnvioDTO dto = null;
            string token = HttpContext.Session.GetString("LogToken");
            try
            {
                var resp = AuxiliarClienteHttp.EnviarSolicitud($"{URLApi}BuscarPorId/{id}", "get", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(resp);
                if (resp.IsSuccessStatusCode)
                {
                    dto = JsonConvert.DeserializeObject<EnvioDTO>(body);
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener los datells del envio.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrio un error en la obtencion del envio";
            }
            return View("DetallesEnvio", dto);
        }

        [HttpGet]
        public ActionResult BuscarPorComentario()
        {
            ViewBag.Comentario = "";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPorComentario(string comentario)
        {
            ViewBag.Comentario = comentario ?? "";
            if (string.IsNullOrEmpty(comentario))
            {
                ViewBag.Error = "Debe ingresar una palabra para buscar";
                return View();
            }

            string email = HttpContext.Session.GetString("LogEmail");
            string token = HttpContext.Session.GetString("LogToken");
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Usuarios");
            }
            try
            {
                var url = $"{URLApi}BuscarPorComentario?Email={email}&Comentario={comentario}";
                var resp = AuxiliarClienteHttp.EnviarSolicitud(url, "get", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(resp);
                if (!resp.IsSuccessStatusCode)
                {
                    ViewBag.Error = $"Error API: {resp.StatusCode} - {body}";
                    return View(new List<EnvioLigthDTO>());
                }
                var lista = JsonConvert
                    .DeserializeObject<List<EnvioLigthDTO>>(body)
                    .OrderByDescending(e => e.FechaRegistroEnvio)
                    .ToList();
                //var ordenada = lista.OrderByDescending(e => e.FechaRegistroEnvio).ToList();
                return View(lista);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrio un error en la obtencion del envio " + ex.Message; ;
                return View(new List<EnvioLigthDTO>());
            }
        }
        [HttpGet]
        public ActionResult DetallesBusquedaComentario(int id)
        {
            EnvioDTO dto = null;
            string token = HttpContext.Session.GetString("LogToken");
            try
            {
                var resp = AuxiliarClienteHttp.EnviarSolicitud($"{URLApi}BuscarPorId/{id}", "get", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(resp);
                if (resp.IsSuccessStatusCode)
                {
                    dto = JsonConvert.DeserializeObject<EnvioDTO>(body);
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener los detalles del envio.";
                    return View("DetallesBusquedaComentario", null);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrio un error en la obtencion del envio";
                return View("DetallesBusquedaComentario", null);
            }
            return View("DetallesBusquedaComentario", dto);
        }
    }
}
