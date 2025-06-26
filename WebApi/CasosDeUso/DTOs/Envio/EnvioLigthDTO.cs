using CasosDeUso.DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.Envio
{
    public class EnvioLigthDTO
    {
        public int Id { get; set; }
        public double Peso { get; set; }
        public string TipoEnvio { get; set; }
        public string EstadoEnvio { get; set; }
        public EmpleadoDTO Empleado { get; set; }
        public string EmailCliente { get; set; }
        public string Destino { get; set; }
        public DateTime FechaRegistroEnvio { get; set; }
        public int Tracking { get; set; }
    }
}