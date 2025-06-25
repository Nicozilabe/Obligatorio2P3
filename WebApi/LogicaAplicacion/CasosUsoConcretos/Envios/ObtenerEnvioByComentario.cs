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
            IEnumerable<EnvioLigthDTO> envios = MapperEnvio.ToListEnvioLigthDTO(repoEnvios.FindAllLightByComentario(datos.Email, datos.Comentario));


            if (envios != null)
            {
                IEnumerable<EnvioLigthDTO> ordenados = envios.OrderBy(e => e.FechaRegistroEnvio);
                envios = ordenados;
            }
            return envios;
        }
    }
}
