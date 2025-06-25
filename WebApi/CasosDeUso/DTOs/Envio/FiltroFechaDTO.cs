using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.Envio
{
    public class FiltroFechaDTO:IValidable
    {

        public DateOnly? FInicio { get; set; }

        public DateOnly? FFin { get; set; }

        public string? Estado { get; set; }

        public void Validar()
        {
            if (FInicio == null && FFin == null && Estado == null)
            {
                throw new DatosInvalidosException("Debe ingresar al menos una fecha o un estado para filtrar.");
            }

            if(FFin != null && FInicio != null && FInicio < FFin)
            {
                throw new DatosInvalidosException("La fecha de fin no puede ser superior a la de inicio.");
            }

            if((Estado != null)&& !(Estado == "Finalizado" || Estado == "En_Proceso"))
            {
                throw new DatosInvalidosException("Estado no válido");
            }

        }
    }
}
