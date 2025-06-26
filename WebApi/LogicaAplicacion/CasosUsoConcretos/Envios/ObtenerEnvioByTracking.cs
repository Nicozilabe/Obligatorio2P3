using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
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
    public class ObtenerEnvioByTracking : IObtenerEnvioByTracking
    {
        IRepositorioEnvios repoEnvios { get; set; }

        public ObtenerEnvioByTracking(IRepositorioEnvios repoEnvios)
        {
            this.repoEnvios = repoEnvios;
        }
        public EnvioDTO getByTracking(int tracking)
        {
            EnvioDTO envio = null;
            Envio en = repoEnvios.FindByTracking(tracking);
            if (en != null)
            {
                envio = MapperEnvio.ToDTO(en);
            }
            return envio;
        }
    }
}