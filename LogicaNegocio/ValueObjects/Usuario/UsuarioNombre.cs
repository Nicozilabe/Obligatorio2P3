using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObjects.Usuario
{
    [Owned]
    public record UsuarioNombre : IValidable
    {
        public string Nombre { get; init; }
        public string Apellido { get; init; }

        public UsuarioNombre(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new DatosInvalidosException("El nombre no debe ser nulo o vacio.");
            }
            if (string.IsNullOrEmpty(Apellido))
            {
                throw new DatosInvalidosException("El apellido no debe ser nulo o vacio.");
            }
            if (Nombre.Length > 25)
            {
                throw new DatosInvalidosException("El nombre debe tener hasta 25 letras.");
            }
            if (Apellido.Length > 25)
            {
                throw new DatosInvalidosException("El apellido debe tener hasta 25 letras.");
            }
        }
    }
}
