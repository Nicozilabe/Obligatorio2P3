using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new DatosInvalidosException("Tipo de envio no válido");
            }
            //if (Tracking <= 0 || Tracking > 99999 || Tracking == null)
            //{
            //    throw new DatosInvalidosException("Tracking debe ser un valor entre 1 y 99999");
            //}
            if (EmpleadoResponable == null)
            {
                throw new DatosInvalidosException("Empleado Responable no válida");
            }
            if (string.IsNullOrEmpty(EmailCliente))
            {
                throw new DatosInvalidosException("El Email-Envio no puede quedar vacio.");
            }
            if (EmailCliente.Length > 32)
            {
                throw new DatosInvalidosException("El Email-Envio debe tener menos de 32 letras");
            }
            if (Peso <= 0 || Peso == null)
            {
                throw new DatosInvalidosException("El Peso-Envio debe ser un valor mayor a 0");
            }
            if (string.IsNullOrEmpty(EstadoEnvio))
            {
                throw new DatosInvalidosException("El EstadoEnvio-Envio no puede quedar vacio.");
            }
            if (EstadoEnvio.Length > 32)
            {
                throw new DatosInvalidosException("El EstadoEnvio-Envio debe contener menos de 32 caracteres.");
            }
            if (Agencia == null)
            {
                throw new DatosInvalidosException("Agencia-Envio no válida");
            }
            if (direccion == null)
            {
                throw new DatosInvalidosException("Direccion-Envio no válida");
            }

            if (FechaEntrega != null)
            {
                if (FechaEntrega < FechaRegistroEnvio)
                {
                    throw new DatosInvalidosException("La fecha de entrega no puede ser menor a la fecha de registro");

                }
                if(FechaEntrega > DateTime.Now)
                {
                    throw new DatosInvalidosException("La fecha de entrega no puede ser mayor a la fecha actual");
                }
            }
                //if (TipoEnvio == "comun")
                //{
                //    Agencia.Validar();
                //}
                //else
                //{
                //    direccion.Validar();
                //}
            }
    }
}
