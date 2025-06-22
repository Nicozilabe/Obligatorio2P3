using CasosDeUso.DTOs;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores;
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
    public class BajaEmpleado : IBajaEmpleado
    {
        public IRepositorioEmpleados repo { get; set; }
        public IRepositorioAcciones repoAcciones { get; set; }

        public BajaEmpleado(IRepositorioEmpleados repo, IRepositorioAcciones repositorioAcciones)
        {
            this.repo = repo;
            repoAcciones = repositorioAcciones;
        }

        public void RelizarBaja(int id, int? idRealizador)
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

            AccionAdministracion accion = new AccionAdministracion(repo.FindById(id), realizador, TipoAccionAdministracion.Baja, new LogicaNegocio.ValueObjects.FechaAccion(DateTime.Now));
            accion.Validar();
            repoAcciones.Add(accion);
            repo.Remove(id);
        }
    }
}
