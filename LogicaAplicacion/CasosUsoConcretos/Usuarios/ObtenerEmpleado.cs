using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUsoConcretos.Usuarios
{
    public class ObtenerEmpleado : IObtenerEmpleado
    {
        IRepositorioEmpleados repo { get; set; }

        public ObtenerEmpleado(IRepositorioEmpleados repo)
        {
            this.repo = repo;
        }

    
        public EmpleadoDTO FindById(int id)
        {
            if (id == null || id <= 0)
            {
                throw new DatosInvalidosException("Id no valido.");
            }

            EmpleadoDTO empleado = MappersEmpleado.ToEmpleadoDTO(repo.FindById(id));

            if (empleado == null)
            {
                throw new DatosInvalidosException("No se encontro el empleado.");
            }

            return empleado;
        }
    }
}
