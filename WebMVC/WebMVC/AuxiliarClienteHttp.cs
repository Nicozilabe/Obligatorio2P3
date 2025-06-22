using System.Net.Http.Headers;

namespace WebMVC
{
    public class AuxiliarClienteHttp
    {
        public static HttpResponseMessage EnviarSolicitud(string url, string verbo, object obj, string token)
        {
            HttpClient cliente = new HttpClient();
            Task<HttpResponseMessage> tarea = null;

            if (token != null) { 
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            if (verbo == "get")
            {
                tarea = cliente.GetAsync(url);
            }
            else if (verbo == "delete")
            {
                tarea = cliente.DeleteAsync(url);
            }
            else if (verbo == "post")
            {
                tarea = cliente.PostAsJsonAsync(url, obj);
            }
            else if (verbo == "put")
            {
                tarea = cliente.PutAsJsonAsync(url, obj);
            }

            tarea.Wait();
            return tarea.Result;
        }

        public static string ObtenerBody(HttpResponseMessage respuesta)
        {
            HttpContent contenido = respuesta.Content;
            Task<string> tarea = contenido.ReadAsStringAsync();
            tarea.Wait();

            return tarea.Result;
        }
    }
}
