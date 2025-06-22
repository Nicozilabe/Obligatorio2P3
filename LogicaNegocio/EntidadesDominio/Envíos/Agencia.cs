using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Envíos
{
    public class Agencia:IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DireccionPostal Direccion { get; set; }
        //se tuvo que separar por el querer tener ciudades como clases
        public int? CiudadId { get; set; }
        public Ciudad Ciudad { get; set; }

        public Ubicacion Ubicacion { get; set; }

        public Agencia() { }

        public Agencia(string nombre, DireccionPostal dir, Ubicacion ubi) {
            Nombre = nombre;
            Direccion = dir;
            Ubicacion = ubi;
        }


        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new DatosInvalidosException("Nombre-Agencia no válido");
            }
            if(Nombre.Length > 32)
            {
                throw new DatosInvalidosException("Nombre debe tener como máximo 32 caracteres");
            }
            if(Direccion == null)
            {
                throw new DatosInvalidosException("Dirección-Agencia no válida");
            }
            if (Ubicacion == null)
            {
                throw new DatosInvalidosException("Ubicación-Agencia no válida");
            }
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}";
        }
    }
}
