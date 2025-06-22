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
    public class EditarEmpleado : IEditarEmpleado
    {
        public IRepositorioEmpleados repo { get; set; }
        public IRepositorioAcciones repoAcciones { get; set; }

        public EditarEmpleado(IRepositorioEmpleados repo, IRepositorioAcciones repoAcciones)
        {
            this.repo = repo;
            this.repoAcciones = repoAcciones;
        }

        void IEditarEmpleado.EditarEmpleado(EmpleadoDTO dto, int? idRealizador)
        {
            if (idRealizador == null)
            {
                throw new DataMisalignedException("El id del realizador no puede ser nulo");
            }

            Administrador realizador = repo.AdministradorPorID((int)idRealizador);

            if (realizador == null)
            {
                throw new PermisosException("La acción solicitada debe ser realizada por un administrador.");
            }

            AccionAdministracion accion = new AccionAdministracion(repo.FindById(dto.Id), realizador, TipoAccionAdministracion.Modificación, new LogicaNegocio.ValueObjects.FechaAccion(DateTime.Now));
            accion.Validar();
            repoAcciones.Add(accion);
            repo.Update(MappersEmpleado.ToEmpleado(dto));
        }
    }
}
