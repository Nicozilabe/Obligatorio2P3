using LogicaNegocio.EntidadesDominio.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioEmpleados : IRepositorio<Empleado>
    {
        Empleado FindByEmail(string email);

        public Administrador AdministradorPorID(int id);
        //public Empleado VerificarEmpleado(int id);
    }
}
