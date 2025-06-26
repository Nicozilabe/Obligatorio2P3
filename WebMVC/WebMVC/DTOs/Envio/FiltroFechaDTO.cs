using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;

namespace CasosDeUso.DTOs.Envio
{
    public class FiltroFechaDTO:IValidable
    {
        public DateOnly? FInicio { get; set; }
        public DateOnly? FFin { get; set; }
        public string? Estado { get; set; }
        public string? Email { get; set; }

        public void Validar()
        {
            if (FInicio == null && FFin == null && Estado == null)
            {
                throw new Exception("Debe ingresar al menos una fecha o un estado para filtrar.");
            }
            if(FFin != null && FInicio != null && FInicio < FFin)
            {
                throw new Exception("La fecha de fin no puede ser superior a la de inicio.");
            }
            if((Estado != null)&& !(Estado == "Finalizado" || Estado == "En_Proceso"))
            {
                throw new Exception("Estado no válido");
            }
            if (Email != null && string.IsNullOrEmpty(Email))
            {
                throw new Exception("El Email no puede quedar vacio.");
            }
        }
    }
}
