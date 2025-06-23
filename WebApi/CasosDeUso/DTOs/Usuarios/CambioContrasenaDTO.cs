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
    public class CambioContrasenaDTO: IValidable
    {

        public string Email { get; set; }
        
        public string PassVieja { get; set; }

        public string PassNueva { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new DatosInvalidosException("El Email no puede quedar vacio.");
            }
            if (string.IsNullOrEmpty(PassVieja))
            {
                throw new DatosInvalidosException("La contraseña no puede quedar vacio.");
            }
            if (string.IsNullOrEmpty(PassNueva))
            {
                throw new DatosInvalidosException("La nueva contraseña no puede quedar vacio.");
            }
        }
    }
}
