using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.Usuarios
{
    public class EmpleadoDTO : IValidable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Nombre no puede superar los 32 caracteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Apellido no puede superar los 32 caracteres.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Email no puede superar los 32 caracteres.")]
        public string Email { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        //no required así se puede no cambiar la contraseña
        public string? Password { get; set; }

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
            if (! string.IsNullOrEmpty(Password))
            {
                if (Password.Length > 32)
                {
                    throw new DatosInvalidosException("La contraseña debe tener menos de 32 letras");
                }
            }
            
            if (Rol != "Empleado" && Rol != "Administrador")
            {
                throw new DatosInvalidosException("Tipo Usuario no válido");
            }
        }

        public override string ToString()
        {
            return $"{Id} {Nombre}, {Apellido}, {Rol}";
        }
    }
}
