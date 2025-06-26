using CasosDeUso.DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCasosUso
{
    public interface ICambiarContraseña
    {
        public void CambiarContraseña(CambioContrasenaDTO datos);
    }
}