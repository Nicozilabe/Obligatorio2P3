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

        public EnvioDTO getByID(int id)
        {
            if (id <= 0)
            {
                throw new DatosInvalidosException("El id no puede ser menor o igual a cero");
            }

            var envio = repoEnvios.FindById(id);
            EnvioDTO ret = null;
            if (envio != null) {
                ret = MapperEnvio.ToDTO(envio);
            }
            return ret;
        }
    }
}