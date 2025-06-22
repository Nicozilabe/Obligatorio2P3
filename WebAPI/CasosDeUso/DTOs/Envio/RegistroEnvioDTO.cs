using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CasosDeUso.DTOs.Envio
{
    public class RegistroEnvioDTO: IValidable
    {
        public int? IdEmpleadoResponable { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [StringLength(32, ErrorMessage = "Email no puede superar los 32 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [DataType(DataType.EmailAddress)]
        public string EmailCliente { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Range(0.1, 9999, ErrorMessage = "El peso debe ser mayor a 0.1 y menor que 9999")]
        public double Peso { get; set; }
        public string TipoEnvio { get; set; }
        public int? IdAgencia { get; set; }
        public DireccionDTO? direccion { get; set; }
        public int IdCiudad { get; set; }

        public void Validar()
        {
            if(IdEmpleadoResponable < 0)
            {
                throw new DatosInvalidosException("Id empleado responsable no valido, debe ser mayor a 0");
            }
            if(IdEmpleadoResponable == null)
            {
                throw new DatosInvalidosException("El Id del empleado responsable no puede quedar vacio");
            }
            if (string.IsNullOrEmpty(EmailCliente))
            {
                throw new DatosInvalidosException("El Email-Registro-Envio no puede quedar vacio.");
            }
            if (EmailCliente.Length > 32)
            {
                throw new DatosInvalidosException("El Email-Registro-Envio debe tener menos de 32 letras");
            }
            if (Peso <= 0 || Peso == null)
            {
                throw new DatosInvalidosException("El Peso-Registro debe ser un valor mayor a 0");
            }
            if (string.IsNullOrEmpty(TipoEnvio))
            {
                throw new DatosInvalidosException("El tipo envio no puede estar vacio");
            }
            if (TipoEnvio != "C" && TipoEnvio != "U")
            {
                throw new DatosInvalidosException("El Tipo unicamente puede ser Comun o Urgente.");
            }
            if (TipoEnvio == "C")
            {
                if (IdAgencia == null)
                {
                    throw new DatosInvalidosException("El Id de la agencia no puede quedar vacio");
                }
                if(IdAgencia < 0)
                {
                    throw new DatosInvalidosException("El Id de la agencia no puede ser menor a 0");
                }
            }
            if (TipoEnvio == "U")
            {
                direccion.Validar();
            }
        }
    }
}
