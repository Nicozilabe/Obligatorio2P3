using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public record FechaAccion:IValidable
    {
        public DateTime Fecha { get; init; }

        protected FechaAccion() { }
        public FechaAccion(DateTime fecha)
        {
            this.Fecha = fecha;
            Validar();
        }

        public void Validar()
        {
            if(Fecha == null)
            {
                throw new DatosInvalidosException("Fecha no válida");
            }
            if(Fecha > DateTime.Now)
            {
                throw new DatosInvalidosException("Fecha/Acción no puede ser superior a la fecha actual");
            }
        }
    }
}