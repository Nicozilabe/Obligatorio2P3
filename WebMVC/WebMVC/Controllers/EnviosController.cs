using CasosDeUso.DTOs.Envio;
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
        public ActionResult Index()
        {
            return View();
        }
        //POST envis
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int tracking)
        {
            EnvioDTO dto = null;
            try
            {

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Detalles", dto);
        }

        //GET detalles
        public ActionResult Detalles(EnvioDTO dto)
        {
            return View(dto);
        }
    }
}
