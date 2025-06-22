using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Envíos
{
    [Index(nameof(Tracking), IsUnique = true)]
    public abstract class Envio : IValidable
    {
        public int Id { get; set; }
        public DateTime FechaRegistroEnvio { get; set; }
        public int Tracking { get; set; }
        public int? EmpleadoResponableId { get; set; }
        public Empleado EmpleadoResponable { get; set; }
        public string Cliente { get; set; }
        public double Peso { get; set; }
        public DateTime? FechaEntrega { get; set; }

        public List<ComentarioEnvio> Comentarios { get; set; } = new List<ComentarioEnvio>();
        public TipoEstadoEnvio EstadoEnvio { get; set; }
        //public TipoSeguimiento Seguimiento { get; set; }


        public Envio() { }

        public Envio(Empleado empleadoResponable, string cliente, double peso, TipoEstadoEnvio estadoEnvio)
        {
            EmpleadoResponable = empleadoResponable;
            Cliente = cliente;
            Peso = peso;
            EstadoEnvio = estadoEnvio;
        }

        public virtual void Validar()
        {
            if (Tracking <= 0 || Tracking > 99999 || Tracking == null)
            {
                throw new DatosInvalidosException("Tracking debe ser un valor entre 1 y 99999");
            }
            if (EmpleadoResponable == null)
            {
                throw new DatosInvalidosException("Empleado Responable no válida");
            }
            if (string.IsNullOrEmpty(Cliente))
            {
                throw new DatosInvalidosException("El Cliente no puede quedar vacio.");
            }
            if (Cliente.Length > 32)
            {
                throw new DatosInvalidosException("El Cliente debe tener menos de 32 letras");
            }
            if (Peso <= 0 || Peso == null)
            {
                throw new DatosInvalidosException("El Peso-Envio debe ser un valor mayor a 0");
            }
            if (EstadoEnvio == null)
            {
                throw new DatosInvalidosException("Estado Envio Responable no válida");
            }
            if (FechaRegistroEnvio == null)
            {
                throw new DatosInvalidosException("Fecha de registro no válida");
            }
        }


        public virtual void finalizarEnvio(DateTime fecha)
        {
            this.EstadoEnvio = TipoEstadoEnvio.Finalizado;
            this.FechaEntrega = fecha;
        }

        public virtual void AgregarComentario(ComentarioEnvio comentario)
        {
            if (comentario == null)
            {
                throw new DatosInvalidosException("Comentario no válido");
            }
            if (Comentarios == null)
            {
                Comentarios = new List<ComentarioEnvio>();
            }
            comentario.Envio = this;
            Comentarios.Add(comentario);
        }
    }
}
