using CasosDeUso.DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;

namespace CasosDeUso.DTOs.Envio
{
    public class EnvioDTO : IValidable
    {
        public int Id { get; set; }
        public int Tracking { get; set; }
        public EmpleadoSeguroDTO EmpleadoResponable { get; set; }
        public string EmailCliente { get; set; }
        public double Peso { get; set; }
        public string EstadoEnvio { get; set; }
        public CiudadDTO Ciudad { get; set; }
        public DateTime FechaRegistroEnvio { get; set; }
        public IEnumerable<ComentarioEnvioDTO> Comentarios { get; set; }
        public string TipoEnvio { get; set; }
        public AgenciaDTO? Agencia { get; set; }
        public DireccionDTO? direccion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public bool EnvioEficiente { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(TipoEnvio) || TipoEnvio != "Comun" || TipoEnvio != "Urgente")
            {
                throw new Exception("Tipo de envio no válido");
            }
            if (EmpleadoResponable == null)
            {
                throw new Exception("Empleado Responable no válida");
            }
            if (string.IsNullOrEmpty(EmailCliente))
            {
                throw new Exception("El Email-Envio no puede quedar vacio.");
            }
            if (EmailCliente.Length > 32)
            {
                throw new Exception("El Email-Envio debe tener menos de 32 letras");
            }
            if (Peso <= 0 || Peso == null)
            {
                throw new Exception("El Peso-Envio debe ser un valor mayor a 0");
            }
            if (string.IsNullOrEmpty(EstadoEnvio))
            {
                throw new Exception("El EstadoEnvio-Envio no puede quedar vacio.");
            }
            if (EstadoEnvio.Length > 32)
            {
                throw new Exception("El EstadoEnvio-Envio debe contener menos de 32 caracteres.");
            }
            if (Agencia == null)
            {
                throw new Exception("Agencia-Envio no válida");
            }
            if (direccion == null)
            {
                throw new Exception("Direccion-Envio no válida");
            }

            if (FechaEntrega != null)
            {
                if (FechaEntrega < FechaRegistroEnvio)
                {
                    throw new Exception("La fecha de entrega no puede ser menor a la fecha de registro");

                }
                if(FechaEntrega > DateTime.Now)
                {
                    throw new Exception("La fecha de entrega no puede ser mayor a la fecha actual");
                }
            }
        }
    }
}