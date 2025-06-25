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
    public class ObtenerEnviosByEmail : IObtenerEnviosByEmail
    {
        IRepositorioEnvios repoEnvios { get; set; }

        public ObtenerEnviosByEmail(IRepositorioEnvios repoEnvios)
        {
            this.repoEnvios = repoEnvios;
        }

        public IEnumerable<EnvioLigthDTO> getEnviosByEmail(string email)
        {
            IEnumerable<Envio> envios = null;
            envios = repoEnvios.FindAllLightByEmailCliente(email);
            IEnumerable<EnvioLigthDTO> ret = null;

            if (envios != null)
            {
                ret= MapperEnvio.ToListEnvioLigthDTO(envios);
            }

            return ret;
        }
    }
}
