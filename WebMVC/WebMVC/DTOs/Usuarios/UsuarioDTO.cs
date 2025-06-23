using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;

namespace WebMVC.DTOs.Usuarios
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
                throw new Exception("El nombre no puede quedar vacio.");
            }
            if (Nombre.Length > 32)
            {
                throw new Exception("El nombre debe tener menos de 32 letras");
            }
            if (string.IsNullOrEmpty(Apellido))
            {
                throw new Exception("El apellido no puede quedar vacio.");
            }
            if (Apellido.Length > 32)
            {
                throw new Exception("El apellido debe tener menos de 32 letras");
            }
            if (string.IsNullOrEmpty(Email))
            {
                throw new Exception("El email no puede quedar vacio.");
            }
            if (Email.Length > 32)
            {
                throw new Exception("El email debe tener menos de 32 letras");
            }
            if (Rol != "Empleado" && Rol != "Administrador" && Rol != "Cliente")
            {
                throw new Exception("Tipo Usuario no válido");
            }
        }
    }
}
