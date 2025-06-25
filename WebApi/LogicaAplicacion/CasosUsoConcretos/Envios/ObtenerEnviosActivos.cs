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
    public class ObtenerEnviosActivos : IObtenerEnviosActivos
    {
        IRepositorioEnvios repoEnvios { get; set; }

        public ObtenerEnviosActivos(IRepositorioEnvios repoEnvios)
        {
            this.repoEnvios = repoEnvios;
        }


        public IEnumerable<EnvioLigthDTO> getEnviosLightActivos()
        {
            IEnumerable<EnvioLigthDTO> envios = MapperEnvio.ToListEnvioLigthDTO(repoEnvios.FindAllLightActivos());

            if (envios == null || envios.Count() == 0)
            {
                throw new DatosInvalidosException("No se encontraron envios activos.");
            }
            return envios;
        }
    }
}
