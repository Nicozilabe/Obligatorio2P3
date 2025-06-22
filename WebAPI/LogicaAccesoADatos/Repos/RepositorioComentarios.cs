using ExcepcionesPropias;
using LogicaAccesoADatos.EF;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoADatos.Repos
{
    public class RepositorioComentarios:IRepositorioComentarios
    {
        public EmpresaContext Context { get; set; }

        public RepositorioComentarios(EmpresaContext context)
        {

            Context = context;

        }

        public void Add(ComentarioEnvio obj)
        {
            if (obj == null)
            {
                throw new DatosInvalidosException("Envío no válido para el alta.");
            }
            Context.Comentarios.Add(obj);
            Context.SaveChanges();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ComentarioEnvio obj)
        {
            throw new NotImplementedException();
        }

        public ComentarioEnvio FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ComentarioEnvio> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
