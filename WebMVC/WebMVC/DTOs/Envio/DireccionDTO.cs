using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CasosDeUso.DTOs.Envio
{
    public class DireccionDTO : IValidable
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Calle no puede superar los 32 caracteres.")]
        public string Calle { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Range(1, 9999, ErrorMessage = "Numero debe ser mayor a 0 y menor que 9999")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Range(1, 99999, ErrorMessage = "Codigo Postal debe ser mayor a 0 y menor que 99999")]
        public int CodigoPostal { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Calle))
            {
                throw new Exception("Calle debe ser un valor válido");
            }
            if (Calle.Length > 32)
            {
                throw new Exception("Calle no debe contener ente 1 y 32 caracteres.");
            }
            if (Numero <= 0 || Numero > 9999 || Numero == null)
            {
                throw new Exception("Número-Dirección Debe ser un valor entre 1 y 9999");
            }
            if (CodigoPostal <= 0 || CodigoPostal > 99999 || CodigoPostal == null)
            {
                throw new Exception("Número-Dirección Debe ser un valor entre 1 y 99999");
            }
        }

        public override string ToString()
        {
            return $"{Calle} {Numero}, {CodigoPostal}";
        }
    }
}
