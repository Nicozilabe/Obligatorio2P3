using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.Enums;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Acciones
{
    public class AccionEnvio : Accion
    {
        public Empleado Realizador { get; set; }
        public TipoAccionEnvio TipoAccion { get; set; }
        public Envio Envio { get; set; }
        public TipoComentarioEnvio ComentarioEnvio { get; set; }

        public AccionEnvio() { }

        public AccionEnvio(TipoAccionEnvio tipoAccion, Envio envio, TipoComentarioEnvio comentarioEnvio, Empleado realizador, FechaAccion fecha):base( fecha)
        {
            Realizador= realizador;
            TipoAccion = tipoAccion;
            Envio = envio;
            ComentarioEnvio = comentarioEnvio;
        }

        public override void Validar()
        {
            base.Validar();
            if(TipoAccion == null)
            {
                throw new DatosInvalidosException("Tipo Acción no válida");
            }
            if (Envio == null) {
                throw new DatosInvalidosException("Envío no válido");
            }
            Envio.Validar();
            if (Realizador == null)
            {
                throw new DatosInvalidosException("Usuario Realizador no válido");
            }
            if (ComentarioEnvio == null)
            {
                throw new DatosInvalidosException("Comentario Énvío no válido");
            }
        }
    }
}