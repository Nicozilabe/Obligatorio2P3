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
    public record UsuarioPassword : IValidable
    {
        public string Password { get; init; }

        public UsuarioPassword(string password) { 
            Password = password;
        }
        protected UsuarioPassword() { }
        public void Validar()
        {
            if (string.IsNullOrEmpty(Password))
            {
                throw new DatosInvalidosException("La contraseña no puede ser nula o vacia.");
            }
            if (Password.Length < 40)
            {
                throw new DatosInvalidosException("La contraseña debe tener hasta 40 caracteres.");
            }
        }
    }
}