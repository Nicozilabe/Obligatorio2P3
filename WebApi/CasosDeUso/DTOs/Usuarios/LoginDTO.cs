using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CasosDeUso.DTOs.Usuarios
{
    public class LoginDTO : IValidable
    {
        public string? Email { get; set; }
        public string? Pass { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new DatosInvalidosException("El Email no puede quedar vacio.");
            }
            if (string.IsNullOrEmpty(Pass))
            {
                throw new DatosInvalidosException("La contraseña no puede quedar vacio.");
            }
        }
    }
}