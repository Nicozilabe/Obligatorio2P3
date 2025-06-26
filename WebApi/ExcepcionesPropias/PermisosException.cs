using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class PermisosException:Exception
    {
        public PermisosException() { }
        public PermisosException(string mensaje) : base(mensaje) { }
        public PermisosException(string mensaje, Exception interna) : base(mensaje, interna) { }
    }
}