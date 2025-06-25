using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Envíos
{
    public class EnvioComun : Envio
    {
        public int? AgenciaId { get; set; }
        public Agencia Agencia { get; set; }

        public EnvioComun(Empleado empleadoResponable, Cliente cliente, double peso, TipoEstadoEnvio estadoEnvio, Agencia agencia) : base(empleadoResponable, cliente, peso, estadoEnvio)
        {
            Agencia = agencia;
        }

        public EnvioComun() { }

        public void Validar()
        {
            base.Validar();
            if (Agencia == null)
            {
                throw new DatosInvalidosException("Agencia no válido");
            }
        }
    }
}
