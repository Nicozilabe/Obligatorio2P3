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
    public record Ubicacion:IValidable
    {
        public double Latitud { get; init; }
        public double Longitud { get; init; }

        protected Ubicacion() { }
        public Ubicacion(double lat, double lon)
        {
            Latitud = lat;
            Longitud= lon;
            Validar();
        }
        public void Validar()
        {
            if (Latitud < -90 || Latitud > 90)
            {
                throw new DatosInvalidosException("Latitud no válida");
            }
            if (Longitud < -180 || Longitud > 180) 
            {
                throw new DatosInvalidosException("Longitud no válida");
            }
            
        }
    }
}
