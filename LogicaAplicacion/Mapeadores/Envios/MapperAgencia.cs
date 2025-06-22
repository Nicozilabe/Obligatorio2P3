using CasosDeUso.DTOs.Envio;
using LogicaNegocio.EntidadesDominio.Envíos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mapeadores.Envios
{
    public class MapperAgencia
    {
        public static AgenciaDTO ToDTO(Agencia a)
        {

            //faltan validaciones
            
            AgenciaDTO ret = new AgenciaDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Direccion = MapperDireccion.ToDTO(a.Direccion),
                Ubicacion = MapperUbicacion.ToDTO(a.Ubicacion),
                Ciudad = MapperCiudad.ToDTO(a.Ciudad),
            };

            return ret;

        }
        public static IEnumerable<AgenciaDTO> ToListDTO(List<Agencia> agencias)
        {
           List<AgenciaDTO> ret = new List<AgenciaDTO>();
            foreach (var agencia in agencias)
            {
                ret.Add(ToDTO(agencia));
            }
            return ret;
        }

        

    }
}
