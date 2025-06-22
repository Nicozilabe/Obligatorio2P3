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
    public class MappersEmpleado
    {
        public static Empleado ToEmpleado(EmpleadoDTO dto)
        {
            Empleado ret = null;
            if (dto.Rol == "Administrador")
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
            if (dto.Password != null)
            {
                ret.Password = new UsuarioPassword(dto.Password);
            }
            ret.Activo = dto.Activo;
            return ret;
        }

        public static EmpleadoDTO ToEmpleadoDTO(Empleado empleado)
        {
            EmpleadoDTO ret = null;
            if (empleado != null)
            {
                ret = new EmpleadoDTO();
                if (empleado is Administrador)
                {
                    ret.Rol = "Administrador";
                }
                else if (empleado is Empleado)
                {
                    ret.Rol = "Empleado";
                }
                else
                {
                    throw new DatosInvalidosException("Rol usuario to DTO Inválido");
                }
                ret.Id = empleado.Id;
                ret.Nombre = empleado.Nombre.Nombre;
                ret.Apellido = empleado.Nombre.Apellido;
                ret.Email = empleado.Email.Email;
                ret.Activo = empleado.Activo;
//No madar la pass a las vistas
                ret.Password = null;
            }
            return ret;
        }

        public static List<EmpleadoDTO> ToListaEmpleadoDTO(List<Empleado> empleados)
        {
            List<EmpleadoDTO> DTOs = new List<EmpleadoDTO>();
            foreach (Empleado empleado in empleados)
            {
                EmpleadoDTO dto = ToEmpleadoDTO(empleado);
                DTOs.Add(dto);
            }
            return DTOs;
        }


        public static EmpleadoSeguroDTO ToEmpleadoSeguroDTO(Empleado empleado)
        {
            EmpleadoSeguroDTO ret = null;
            if (empleado != null)
            {
                ret = new EmpleadoSeguroDTO();
                if (empleado is Administrador)
                {
                    ret.Rol = "Administrador";
                }
                else if (empleado is Empleado)
                {
                    ret.Rol = "Empleado";
                }
                else
                {
                    throw new DatosInvalidosException("Rol usuario to DTO Inválido");
                }
                ret.Id = empleado.Id;
                ret.Nombre = empleado.Nombre.Nombre;
                ret.Apellido = empleado.Nombre.Apellido;
                ret.Email = empleado.Email.Email;
                ret.Activo = empleado.Activo;
                
            }
            return ret;
        }
    }
}
