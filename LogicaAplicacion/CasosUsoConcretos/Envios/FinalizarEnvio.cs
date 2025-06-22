using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUsoConcretos.Envios
{
    public class FinalizarEnvio : IFinalizarEnvio
    {
        IRepositorioEnvios repoEnvio;

        public FinalizarEnvio(IRepositorioEnvios repoEnvio)
        {
            this.repoEnvio = repoEnvio;
        }

        public void finalizarEnvio(int idEnvio, DateTime fecha)
        {
            if (idEnvio <= 0)
            {
                throw new DatosInvalidosException("El id del envío no es válido");
            }
            if (fecha == null)
            {
                throw new DatosInvalidosException("La fecha no es válida");
            }
            if (fecha > DateTime.Now)
            {
                throw new DatosInvalidosException("La fecha no puede ser mayor a la fecha actual");
            }
            Envio e = repoEnvio.FindById(idEnvio);
            if (e == null)
            {
                throw new DatosInvalidosException("El envío no existe");
            }
            else
            {
                if (e.EstadoEnvio == TipoEstadoEnvio.Finalizado)
                {
                    throw new DatosInvalidosException("El envío ya fue entregado");
                }
                if(e.FechaRegistroEnvio > fecha)
                {
                    throw new DatosInvalidosException("La fecha no puede ser anterior a la fecha de registro");
                }
                e.finalizarEnvio(fecha);
                repoEnvio.Update(e);
            }

        }
    }
    
}
