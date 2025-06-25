using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
using LogicaAplicacion.Mapeadores.Envios;
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
            IEnumerable<EnvioLigthDTO> envios = MapperEnvio.ToListEnvioLigthDTO(repoEnvios.FindAllLightByEmailCliente(email));


            if (envios != null)
            {
                IEnumerable<EnvioLigthDTO> ordenados = envios.OrderBy(e => e.FechaRegistroEnvio);
                envios = ordenados;
            }
            return envios;
        }
    }
}
