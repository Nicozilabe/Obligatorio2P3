using CasosDeUso.DTOs.Envio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mapeadores.Envios
{
    public class MapperUbicacion
    {

        public static UbicacionDTO ToDTO(Ubicacion u)
        {
            //faltan validaciones
            UbicacionDTO ret = new UbicacionDTO
            {
                Latitud = u.Latitud,
                Longitud = u.Longitud
            };

            return ret;
        }



    }
}
