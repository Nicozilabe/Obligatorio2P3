using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CasosDeUso.DTOs.Usuarios
{
    public class LoginDTO : IValidable
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Email no puede superar los 32 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Contraseña no puede superar los 32 caracteres.")]
        public string Pass { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new Exception("El Email no puede quedar vacio.");
            }
            if (string.IsNullOrEmpty(Pass))
            {
                throw new Exception("La contraseña no puede quedar vacio.");
            }
        }
    }
}
