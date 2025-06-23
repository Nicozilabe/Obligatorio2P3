using WebMVC.DTOs.Envio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Runtime.Intrinsics.X86;
using Newtonsoft.Json;

namespace WebMVC.Controllers
{
    public class EnviosController: Controller
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
                HttpResponseMessage resultado = AuxiliarClienteHttp.EnviarSolicitud(URLApi + "RutaBuscarPorTracking/" + tracking, "get", null, null);
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
    }
}
