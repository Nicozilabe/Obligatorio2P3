using CasosDeUso.DTOs.Usuarios;
using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mapeadores.Usuarios
{
    public class MappersRegistro
    {
        public static Usuario ToUsuario(RegistroEmpleadoDTO dto)
        {

            Usuario ret = null;
            if (dto.Rol == "Cliente")
            {
                ret = new Cliente();
            }
            else if (dto.Rol == "Administrador")
            {
                ret = new Administrador();
            }
            else if (dto.Rol == "Empleado")
            {
                ret = new Empleado();
            }



            ret.Nombre = new UsuarioNombre(dto.Nombre, dto.Apellido);
            ret.Email = new UsuarioEmail(dto.Email);
            ret.Password = new UsuarioPassword(dto.Pass);
            return ret;
        }
    }
}
