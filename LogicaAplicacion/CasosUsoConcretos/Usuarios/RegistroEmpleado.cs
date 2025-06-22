using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Usuarios;
using LogicaNegocio.EntidadesDominio.Acciones;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUsoConcretos.Usuarios
{
    public class RegistroEmpleado : IRegistroEmpleado
    {
        public IRepositorioUsuarios RepoUsuarios { get; set; }
        public IRepositorioAcciones RepoAcciones { get; set; }
        public IRepositorioEmpleados RepoEmpleados { get; set; }

        public RegistroEmpleado() { }

        public RegistroEmpleado(IRepositorioUsuarios repoUsuarios, IRepositorioAcciones repoAcciones, IRepositorioEmpleados repoEmpleados)
        {
            RepoUsuarios = repoUsuarios;
            RepoAcciones = repoAcciones;
            RepoEmpleados = repoEmpleados;
        }

        public UsuarioDTO RegistrarEmpleado(RegistroEmpleadoDTO datos)
        {

            datos.Validar();
            if (datos.IdRealizador == null)
            {
                throw new DataMisalignedException("El id del realizador no puede ser nulo");
            }
            Administrador realizador = RepoEmpleados.AdministradorPorID(datos.IdRealizador);
            
            if (realizador == null)
            {
                throw new PermisosException("La acción solicitada debe ser realizada por un administrador.");
            }

            RepoUsuarios.Add(MappersRegistro.ToUsuario(datos));
            Usuario creado = RepoUsuarios.FindByEmail(datos.Email);
            Empleado creadoCast = null;

            if (creado is Empleado c)
            {
                creadoCast = c;
            }
            else
            {
                throw new DatosInvalidosException("Eror al crear el empleado");
            }

            Accion accion = new AccionAdministracion(creadoCast, realizador, TipoAccionAdministracion.Registro, new LogicaNegocio.ValueObjects.FechaAccion(DateTime.Now));
            accion.Validar();
            RepoAcciones.Add(accion);
            return MappersUsuario.ToUsuarioDTO(creado);

        }
    }
}
