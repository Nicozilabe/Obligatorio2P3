using CasosDeUso.DTOs.Envio;
using LogicaNegocio.EntidadesDominio.Envíos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mapeadores.Envios
{
    public class MapperCiudad
    {
        public static CiudadDTO ToDTO(Ciudad c)
        {
            //faltan validaciones
            CiudadDTO ret = new CiudadDTO
            {
                Nombre = c.Nombre,
                Id = c.Id,
            };

            return ret;
        }

        public static IEnumerable<CiudadDTO> ToListDTO(IEnumerable<Ciudad> ciudades)
        {
            List<CiudadDTO> ret = new List<CiudadDTO>();
            foreach (var ciudad in ciudades)
            {
                ret.Add(ToDTO(ciudad));
            }
            return ret;
        }
    }
}
