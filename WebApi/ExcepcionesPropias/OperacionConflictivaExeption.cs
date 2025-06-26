using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class OperacionConflictivaExeption:Exception
    {
        public OperacionConflictivaExeption() { }
        public OperacionConflictivaExeption(string mensaje) : base(mensaje) { }
        public OperacionConflictivaExeption(string mensaje, Exception interna) : base(mensaje, interna) { }
    }
}