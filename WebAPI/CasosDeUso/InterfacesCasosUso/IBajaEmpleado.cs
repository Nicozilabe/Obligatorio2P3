using CasosDeUso.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCasosUso
{
    public interface IBajaEmpleado
    {
        public void RelizarBaja(int id, int? idRealizador);
    }
}
