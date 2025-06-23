using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;

namespace WebMVC.DTOs.Envio
{
    public class CiudadDTO: IValidable
    {   
        public int Id { get; init; }
        public string Nombre { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre-ciudad no puede quedar vacio.");
            }
            if (Nombre.Length > 32)
            {
                throw new Exception("El nombre-ciudad debe tener menos de 32 letras");
            }
        }
        public override string ToString()
        {
            return $"{Nombre}";
        }
    }
}
