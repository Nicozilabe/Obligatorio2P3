using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System.ComponentModel.DataAnnotations;

namespace CasosDeUso.DTOs.Envio
{
    public class ComentarioEnvioDTO:IValidable
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Comentario no puede superar los 32 caracteres.")]
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public EmpleadoSeguroDTO Empleado { get; set; }
        public int? EmpleadoId { get; set; }
        public ComentarioEnvioDTO() { }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Comentario))
            {
                throw new DatosInvalidosException("El comentario no puede quedar vacio.");
            }
            if(Comentario.Length > 32)
            {
                throw new DatosInvalidosException("El comentario debe tener menos de 32 letras");
            }
            if(Fecha == null)
            {
                throw new DatosInvalidosException("La fecha no puede quedar vacia.");
            }
            if(EmpleadoId <= 0)
            {
                throw new DatosInvalidosException("Id Empleado no válido");
            }
        }
    }
}