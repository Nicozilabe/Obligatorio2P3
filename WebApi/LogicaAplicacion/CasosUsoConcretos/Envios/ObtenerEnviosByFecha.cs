using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Envios;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUsoConcretos.Envios
{
    public class ObtenerEnviosByFecha : IObtenerEnviosByFecha
    {
        IRepositorioEnvios repoEnvios { get; set; }

        public ObtenerEnviosByFecha(IRepositorioEnvios repoEnvios)
        {
            this.repoEnvios = repoEnvios;
        }
        public IEnumerable<EnvioLigthDTO> getEnviosByFecha(FiltroFechaDTO datos)
        {
            IEnumerable<Envio> Envios = null;
            IEnumerable < EnvioLigthDTO > Ret = null;

            if (datos == null)
            {
                throw new DatosInvalidosException( "Los datos de filtro no pueden ser nulos.");
            }
            datos.Validar();

             Envios = repoEnvios.FindByFecha(datos.Email, datos.FInicio, datos.FFin, datos.Estado);

            if(Envios != null)
            {
                Ret = MapperEnvio.ToListEnvioLigthDTO(Envios);
            }

            return Ret;

        }
    }
}
