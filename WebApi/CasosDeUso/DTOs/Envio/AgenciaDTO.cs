using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.Envio
{
    public class AgenciaDTO : IValidable
    {
        public int Id { get; init; }
        public string Nombre { get; set; }
        public DireccionDTO Direccion { get; set; }
        public UbicacionDTO Ubicacion { get; set; }
        public CiudadDTO Ciudad { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new DatosInvalidosException("El Nombre-Agencia no puede quedar vacio.");
            }
            if (Nombre.Length > 32)
            {
                throw new DatosInvalidosException("El Nombre-Agencia debe tener menos de 32 letras");
            }
            if (Direccion == null)
            {
                throw new DatosInvalidosException("Direccion no válida");
            }
            if (Ubicacion == null)
            {
                throw new DatosInvalidosException("Ubicacion no válida");
            }
        }

        public override string ToString()
        {
            return $"{Nombre}, {Ciudad}, {Direccion}";
        }
    }
}