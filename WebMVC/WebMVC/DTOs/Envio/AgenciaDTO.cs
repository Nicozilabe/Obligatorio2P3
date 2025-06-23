using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;

namespace WebMVC.DTOs.Envio
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
                throw new Exception("El Nombre-Agencia no puede quedar vacio.");
            }
            if (Nombre.Length > 32)
            {
                throw new Exception("El Nombre-Agencia debe tener menos de 32 letras");
            }
            if (Direccion == null)
            {
                throw new Exception("Direccion no válida");
            }
            if (Ubicacion == null)
            {
                throw new Exception("Ubicacion no válida");
            }
        }

        public override string ToString()
        {
            return $"{Nombre}, {Ciudad}, {Direccion}";
        }
    }
}
