using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new DatosInvalidosException("El Email-Envio no puede quedar vacio.");
            }
            if (string.IsNullOrEmpty(Comentario))
            {
                throw new DatosInvalidosException("El Email-Envio no puede quedar vacio.");
            }
        }
    }
}
