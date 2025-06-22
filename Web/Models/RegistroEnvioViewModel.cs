using CasosDeUso.DTOs.Envio;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegistroEnvioViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Email no puede superar los 32 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [DataType(DataType.EmailAddress)]
        public string EmailCliente { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Range(0.1, 9999, ErrorMessage = "El peso debe ser mayor a 0.1 y menor que 9999")]
        public double Peso { get; set; }
        public string TipoEnvio { get; set; }
        public int IdAgencia { get; set; }
        public int IdCiudad { get; set; }
        public DireccionDTO? direccion { get; set; }

        public IEnumerable<CiudadDTO> Ciudades { get; set; }
        public IEnumerable<AgenciaDTO> Agencias { get; set; } 
    }
}
