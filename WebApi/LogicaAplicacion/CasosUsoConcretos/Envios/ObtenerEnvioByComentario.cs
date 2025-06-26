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
    public class ObtenerEnvioByComentario : IObtenerEnvioByComentario
    {
        IRepositorioEnvios repoEnvios { get; set; }

        public ObtenerEnvioByComentario(IRepositorioEnvios repoEnvios)
        {
            this.repoEnvios = repoEnvios;
        }

        public IEnumerable<EnvioLigthDTO> getEnviosByComentario(FiltroComentarioDTO datos)
        {
            datos.Validar();
            IEnumerable<Envio> envios = repoEnvios.FindAllLightByComentario(datos.Email, datos.Comentario);
            IEnumerable<EnvioLigthDTO> ret = null;

            if (envios != null)
            {
                ret = MapperEnvio.ToListEnvioLigthDTO(envios);
            }
            return ret;
        }
    }
}