using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.DTOs.Usuarios
{
    public class EmpleadoSeguroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }

        public override string ToString()
        {
            return $"{Id} {Nombre}, {Apellido}, {Rol}";
        }
    }
}
