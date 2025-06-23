using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;

namespace CasosDeUso.DTOs.Envio
{
    public class UbicacionDTO: IValidable
    {
        public double Latitud { get; init; }
        public double Longitud { get; init; }

        public void Validar()
        {
            if (Latitud < -90 || Latitud > 90)
            {
                throw new Exception("Latitud no válida");
            }
            if (Longitud < -180 || Longitud > 180)
            {
                throw new Exception("Longitud no válida");
            }
        }
    }
}
