using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;

namespace CasosDeUso.DTOs.Envio
{
    public class FiltroComentarioDTO:IValidable
    {
        public string? Email { get; set; }
        public string? Comentario { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new Exception("El Email-Envio no puede quedar vacio.");
            }
            if (string.IsNullOrEmpty(Comentario))
            {
                throw new Exception("El Email-Envio no puede quedar vacio.");
            }
        }
    }
}
