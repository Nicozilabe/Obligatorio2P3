using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.Enums;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Envíos
{
    public class EnvioUrgente : Envio
    {
        //se tuvo que separar por el querer tener ciudades como clases
        public int? CiudadId { get; set; }
        public Ciudad Ciudad { get; set; }

        public DireccionPostal Direccion { get; set; }
        public bool EnvioEficiente { get; set; }

        public EnvioUrgente() { }

        public EnvioUrgente(Empleado empleadoResponable, Cliente cliente, double peso, TipoEstadoEnvio estadoEnvio, TipoSeguimiento seguimiento, DireccionPostal direccion) : base(empleadoResponable, cliente, peso, estadoEnvio)
        {
            Direccion = direccion;
        }

        public void Validar()
        {
            base.Validar();
            if (Ciudad == null)
            {
                throw new DatosInvalidosException("Ciduad no válida");
            }
            if (Direccion == null)
            {
                throw new DatosInvalidosException("Direccion no válida");
            }
        }

        public override void finalizarEnvio(DateTime fecha) { 
            base.finalizarEnvio(fecha);
            TimeSpan diferencia = fecha - FechaRegistroEnvio;
            this.EnvioEficiente = diferencia.TotalHours < 24;

        }
    }
}
