using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
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
    public class ObtenerEnvio : IObtenerEnvio
    {
        IRepositorioEnvios repoEnvios { get; set; }

        public ObtenerEnvio(IRepositorioEnvios repoEnvios)
        {
            this.repoEnvios = repoEnvios;
        }


        //Cargar menos a la base con datos que no se van a mostrar
        public IEnumerable<EnvioLigthDTO> getEnviosLightActivos()
        {
            IEnumerable<EnvioLigthDTO> envios = MapperEnvio.ToListEnvioLigthDTO(repoEnvios.FindAllLightActivos());

            if (envios == null || envios.Count() == 0)
            {
                throw new DatosInvalidosException("No se encontraron envios activos.");
            }
            return envios;
        }

        public EnvioDTO getByID(int id)
        {
            if (id <= 0)
            {
                throw new DatosInvalidosException("El id no puede ser menor o igual a cero");
            }
            var envio = repoEnvios.FindById(id);
          
            
            return MapperEnvio.ToDTO(envio);
            
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
