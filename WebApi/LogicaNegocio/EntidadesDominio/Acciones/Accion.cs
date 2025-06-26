using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Acciones
{
    public abstract class Accion:IValidable
    {
        public int Id { get; set; }
        public FechaAccion Fecha { get; set; }

        public Accion() { }

        public Accion( FechaAccion fecha)
        {
            Fecha = fecha;
        }

        public virtual void Validar()
        {
            if(Fecha == null)
            {
                throw new DatosInvalidosException("Fecha/Acción no válida(Construcción Acción)");
            } ;
        }
    }
}