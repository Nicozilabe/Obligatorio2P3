using LogicaAccesoADatos.EF;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoADatos.Repos
{
    public class RepositorioCiudades : IRepositorioCiudades
    {
        public EmpresaContext Context { get; set; }
        public RepositorioCiudades(EmpresaContext context)
        {
            Context = context;
        }
        public void Add(Ciudad obj)
        {
            throw new NotImplementedException();
        }

        public List<Ciudad> FindAll()
        {
            return Context.Ciudades.ToList();
        }

        public Ciudad FindById(int id)
        {
            Ciudad buscado = Context.Ciudades.Where(Ciudad => Ciudad.Id == id).SingleOrDefault();
            return buscado;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Ciudad obj)
        {
            throw new NotImplementedException();
        }
    }


}
