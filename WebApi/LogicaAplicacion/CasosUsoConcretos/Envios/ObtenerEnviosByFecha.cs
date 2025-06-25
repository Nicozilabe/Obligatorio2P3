using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Envios;
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

            if (datos == null)
            {
                throw new DatosInvalidosException( "Los datos de filtro no pueden ser nulos.");
            }
            datos.Validar();

            IEnumerable<EnvioLigthDTO> Envios = MapperEnvio.ToListEnvioLigthDTO(repoEnvios.FindByFecha(datos.Email, datos.FInicio, datos.FFin, datos.Estado));
            IEnumerable<EnvioLigthDTO> Ret = null;

            if (Envios != null && Envios.Count() > 0)
            {
                Ret = Envios.OrderBy(e => e.Tracking);
            }

            return Ret;

        }
    }
}
