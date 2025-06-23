using CasosDeUso.DTOs.Envio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCasosUso
{
    public interface IObtenerEnvio
    {
        public IEnumerable<EnvioLigthDTO> getEnviosLightActivos();
        public EnvioDTO getByID(int id);
        public EnvioDTO getByTracking(int tracking);
        public IEnumerable<EnvioLigthDTO> getEnviosByEmail(string email);
        public IEnumerable<EnvioLigthDTO> getEnviosByComentario(FiltroComentarioDTO datos);
    }
}
