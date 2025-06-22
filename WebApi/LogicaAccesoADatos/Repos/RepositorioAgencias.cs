using LogicaAccesoADatos.EF;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoADatos.Repos
{
    public class RepositorioAgencias : IRepositorioAgencias
    {
        public EmpresaContext Context { get; set; }

        public RepositorioAgencias(EmpresaContext context)
        {

            Context = context;

        }
        public void Add(Agencia obj)
        {
            throw new NotImplementedException();
        }

        public List<Agencia> FindAll()
        {
            return Context.Agencias.Include(a => a.Ubicacion).Include(a => a.Direccion).Include(a => a.Ciudad).ToList();
        }

        public Agencia FindById(int id)
        {
            Agencia buscado = Context.Agencias.Where(Agencia => Agencia.Id == id).SingleOrDefault();
            return buscado;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Agencia obj)
        {
            throw new NotImplementedException();
        }
    }
}
