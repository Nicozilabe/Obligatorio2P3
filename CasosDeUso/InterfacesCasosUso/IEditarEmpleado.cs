using CasosDeUso.DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCasosUso
{
    public interface IEditarEmpleado
    {
        public void EditarEmpleado(EmpleadoDTO dto, int? idRealizador);
    }
}
