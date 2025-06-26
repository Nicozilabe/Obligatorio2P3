using LogicaNegocio.EntidadesDominio.Envíos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioEnvios:IRepositorio<Envio>
    {
        public IEnumerable<Envio> FindAllLightActivos();
        public Envio FindByTracking(int tracking);
        IEnumerable<Envio> FindAllLightByEmailCliente(string eCliente);
        IEnumerable<Envio> FindAllLightByComentario(string eCliente, string comentario);
        IEnumerable<Envio> FindByFecha(string email, DateTime? fechaDesde, DateTime? fechaHasta, string? estado);
    }
}