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
    public class MappersUsuario
    {
        public static Usuario ToUsuario(UsuarioDTO dto)
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
            ret.Id = dto.Id;
            ret.Nombre = new UsuarioNombre(dto.Nombre, dto.Apellido);
            ret.Email = new UsuarioEmail(dto.Email);
            ret.Password = null;
            ret.Activo = dto.Activo;
            return ret;
        }

        public static UsuarioDTO ToUsuarioDTO(Usuario usuario)
        {
            UsuarioDTO ret = null;
            if (usuario != null)
            {
                ret = new UsuarioDTO();
                if (usuario is Administrador)
                {
                    ret.Rol = "Administrador";
                }
                else if (usuario is Cliente)
                {
                    ret.Rol = "Cliente";
                }
                else if (usuario is Empleado)
                {
                    ret.Rol = "Empleado";
                }
                else
                {
                    throw new DatosInvalidosException("Rol usuario to DTO Inválido");
                }
                ret.Id = usuario.Id;
                ret.Nombre = usuario.Nombre.Nombre;
                ret.Apellido = usuario.Nombre.Apellido;
                ret.Email = usuario.Email.Email;
                ret.Activo = usuario.Activo;
            }
            return ret;
        }

        public static List<UsuarioDTO> ToListaUsuarioDTO(List<Usuario> usuarios)
        {
            List<UsuarioDTO> DTOs = new List<UsuarioDTO>();
            foreach (Usuario usuario in usuarios)
            {
                UsuarioDTO dto = ToUsuarioDTO(usuario);
                DTOs.Add(dto);
            }
            return DTOs;
        }
    }
}