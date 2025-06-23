using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;

namespace CasosDeUso.DTOs.Usuarios
{
    public class RegistroEmpleadoDTO : IValidable
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Nombre no puede superar los 32 caracteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Email no puede superar los 32 caracteres.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Apellido no puede superar los 32 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Rol { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Contraseña no puede superar los 32 caracteres.")]
        [MinLength(5, ErrorMessage = "El campo debe tener al menos 5 caracteres.")]
        public string Pass { get; set; }
        public int IdRealizador { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede quedar vacio. DTO->Usuario");
            }
            if (Nombre.Length > 32)
            {
                throw new Exception("El nombre debe tener menos de 32 letras. DTO->Usuario");
            }
            if (string.IsNullOrEmpty(Apellido))
            {
                throw new Exception("El apellido no puede quedar vacio. DTO->Usuario");
            }
            if (Apellido.Length > 32)
            {
                throw new Exception("El apellido debe tener menos de 32 letras. DTO->Usuario");
            }
            if (string.IsNullOrEmpty(Email))
            {
                throw new Exception("El email no puede quedar vacio. DTO->Usuario");
            }
            if (Email.Length > 32)
            {
                throw new Exception("El email debe tener menos de 32 letras. DTO->Usuario");
            }
            if (string.IsNullOrEmpty(Pass))
            {
                throw new Exception("La contraseña no puede quedar vacio. DTO->Usuario");
            }
            if (Pass.Length > 32)
            {
                throw new Exception("La contraseña debe tener menos de 32 letras. DTO->Usuario");
            }
            if (Rol != "Empleado" && Rol != "Administrador")
            {
                throw new Exception("Tipo Usuario no válido. DTO->Usuario");
            }
            if (IdRealizador < 0)
            {
                throw new Exception("Id Realizador no válido. DTO->Usuario");
            }
        }
    }
}