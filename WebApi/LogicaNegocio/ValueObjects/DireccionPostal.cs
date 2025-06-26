using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public record DireccionPostal : IValidable
    {
        public string Calle { get; init; }
        public int Numero { get; init; }
        public int CodigoPostal { get; init; }

        protected DireccionPostal() { 
            
        }
        public DireccionPostal(string calle, int numero, int cp) {
            Calle = calle;
            Numero = numero;
            CodigoPostal = cp;
            Validar();
        }
        public void Validar()
        {
            if (string.IsNullOrEmpty(Calle)) {
                throw new DatosInvalidosException("Calle debe ser un valor válido");
            }
            if(Calle.Length > 32)
            {
                throw new DatosInvalidosException("Calle no debe contener ente 1 y 32 caracteres.");
            }
            if (Numero <= 0 || Numero > 9999 || Numero == null) {
                throw new DatosInvalidosException("Número-Dirección Debe ser un valor entre 1 y 9999");
            }
            if (CodigoPostal <= 0 || CodigoPostal > 99999 || CodigoPostal == null)
            {
                throw new DatosInvalidosException("Número-Dirección Debe ser un valor entre 1 y 99999");
            }
           
        }

        public override string ToString()
        {
            return $"{Calle} {Numero}, {CodigoPostal}";
        }
    }
}
