using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
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
            datos.Validar();
        }
    }
}
