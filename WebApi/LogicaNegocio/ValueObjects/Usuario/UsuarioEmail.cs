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
    public record UsuarioEmail : IValidable
    {
        public string Email { get; init; }

        public UsuarioEmail(string email)
        {
            Email = email;
        }
        protected UsuarioEmail() { }
        public void Validar()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new DatosInvalidosException("El email no puede estar vacio.");
            }
            if (Email.Length < 86)
            {
                throw new DatosInvalidosException("El email debe tener hasta 86 caracteres.");
            }
        }

        public override string ToString()
        {
            return Email;
        }
    }
}