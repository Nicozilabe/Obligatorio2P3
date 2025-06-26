using CasosDeUso.DTOs.Envio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCasosUso
{
    public interface IObtenerEnviosByFecha
    {
        public IEnumerable<EnvioLigthDTO> getEnviosByFecha(FiltroFechaDTO datos);
    }
}