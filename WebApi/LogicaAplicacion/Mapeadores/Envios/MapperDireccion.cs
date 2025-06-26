using CasosDeUso.DTOs.Envio;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mapeadores.Envios
{
    public class MapperDireccion
    {
        public static DireccionDTO ToDTO(DireccionPostal d)
        {
            //faltan validaciones
            DireccionDTO ret = new DireccionDTO
            {
                Calle = d.Calle,
                Numero = d.Numero,
            };
            return ret;
        }

        public static DireccionPostal ToDireccion(DireccionDTO dto)
        {
            DireccionPostal ret = new DireccionPostal(dto.Calle, dto.Numero, dto.CodigoPostal);
            
            return ret;
        }
    }
}