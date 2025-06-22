using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.Enums;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Acciones
{
    public class AccionAdministracion : Accion
    {

        public int RealizadorId { get; set; }
        public int? AfectadoId { get; set; }
        public Empleado? Afectado { get; set; }


        public Administrador Realizador { get; set; }
        public TipoAccionAdministracion TipoAccion { get; set; }

        public AccionAdministracion() { }

        public AccionAdministracion(Empleado afectado, Administrador realizador, TipoAccionAdministracion tipoAccion, FechaAccion fecha):base(fecha)
        {
            Afectado = afectado;
            Realizador = realizador;
            TipoAccion = tipoAccion;
        }
        public override void Validar()
        {
            base.Validar();
            if(TipoAccion == null){
                throw new DatosInvalidosException("Tipo acción no válida");
            }
            if (Realizador == null) {
                throw new DatosInvalidosException("Usuario Realizador no válido");
            }
        }
    }
}
