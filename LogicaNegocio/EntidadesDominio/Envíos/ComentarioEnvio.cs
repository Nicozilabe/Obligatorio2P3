using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Envíos
{
    public class ComentarioEnvio
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public Empleado Empleado { get; set; }
        public int? EmpleadoId { get; set; }
        public int? EnvioID { get; set; }
        public Envio Envio { get; set; }

        public ComentarioEnvio() { }
    }
}
