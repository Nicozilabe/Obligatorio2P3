using CasosDeUso.Enums;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.Usuarios
{
    public class UsuarioDTO : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new DatosInvalidosException("El nombre no puede quedar vacio.");
            }
            if (Nombre.Length > 32)
            {
                throw new DatosInvalidosException("El nombre debe tener menos de 32 letras");
            }
            if (string.IsNullOrEmpty(Apellido))
            {
                throw new DatosInvalidosException("El apellido no puede quedar vacio.");
            }
            if (Apellido.Length > 32)
            {
                throw new DatosInvalidosException("El apellido debe tener menos de 32 letras");
            }
            if (string.IsNullOrEmpty(Email))
            {
                throw new DatosInvalidosException("El email no puede quedar vacio.");
            }
            if (Email.Length > 32)
            {
                throw new DatosInvalidosException("El email debe tener menos de 32 letras");
            }
            if (Rol != "Empleado" && Rol != "Administrador" && Rol != "Cliente")
            {
                throw new DatosInvalidosException("Tipo Usuario no válido");
            }
        }
    }
}
