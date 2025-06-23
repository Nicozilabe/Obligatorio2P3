using WebMVC.DTOs.Envio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Runtime.Intrinsics.X86;

namespace WebMVC.Controllers
{
    public class EnviosController: Controller
    {
        public string URLApi { get; set; }

        public EnviosController(IConfiguration config) 
        {
            URLApi = config.GetValue<string>("UrlApiEnvios");
        }

        //RF1 – Dado u número de tracking de un envío obtener todos los detalles de ese envío (Sin autenticación)
        //Se ingresará el número de tracking y obtendrá todo el detalle del envío, incluyendo los seguimientos que se le
        //han realizado.Este requerimiento no debe estar restringido.Se espera que funcione como una forma rápida de
        //consultar el estado de un envío


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
                HttpResponseMessage resultado = AuxiliarClienteHttp.EnviarSolicitud(URLApi + tracking, "get", null, null);
                //string body = AuxiliarClienteHttp.ObtenerBody(resultado);

                if (resultado.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Seguimiento));
                }
                else
                {
                    ViewBag.Error = AuxiliarClienteHttp.ObtenerBody(resultado);
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
