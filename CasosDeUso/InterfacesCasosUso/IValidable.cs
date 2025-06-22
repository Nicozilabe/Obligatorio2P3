using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.InterfacesCasosUso
{
    public interface IValidable
    {
        //la creamos de nuevo acá para no tener referencia a lógica de negocio
        public void Validar();
    }
}
