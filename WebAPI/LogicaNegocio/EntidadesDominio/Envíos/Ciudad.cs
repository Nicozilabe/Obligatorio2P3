using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Envíos
{

    public class Ciudad : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; init; }

        public Ciudad(string nombre)
        {
            Nombre = nombre;
            Validar();
        }
        public Ciudad() { }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new DatosInvalidosException("Nombre-Ciudad no válido");
            }
            if (Nombre.Length > 32)
            {
                throw new DatosInvalidosException("Nombre-Ciudad debe ser un string de 1 a 32 caracteres");
            }
        }
        public override string ToString()
        {
            return $"Nombre: {Nombre}";
        }
    }
}
