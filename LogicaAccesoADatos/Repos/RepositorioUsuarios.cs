using ExcepcionesPropias;
using LogicaAccesoADatos.EF;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoADatos.Repos
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {

        public EmpresaContext Context { get; set; }

        public RepositorioUsuarios(EmpresaContext context)
        {

            Context = context;

        }

        public void Add(Usuario obj)
        {
            if (obj == null)
            {
                throw new DatosInvalidosException("Usuario no válido para el alta.");
            }
            obj.Validar();

            Usuario buscado = FindByEmail(obj.Email.Email);

            if (buscado != null)
            {
                throw new DatosInvalidosException("El Usuario ya existe");
            }
            Context.Usuarios.Add(obj);
            Context.SaveChanges();
        }

        public List<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindByEmail(string email)
        {
            Usuario? buscado = Context.Usuarios.AsEnumerable().Where(Usuario => Usuario.Email.Email == email).SingleOrDefault();
            return buscado;
        }

        public Usuario FindById(int id)
        {
            Usuario? buscado = Context.Usuarios.Where(Usuario => Usuario.Id == id).SingleOrDefault();
            return buscado;
        }

        public void Remove(int id)
        {
            Usuario aBorrar = FindById(id);
            if (aBorrar == null)
            {
                throw new DatosInvalidosException("No hay un Empleado para borrar aqui");
            }
            Context.Usuarios.Remove(aBorrar);
            Context.SaveChanges();
        }

        public void Update(Usuario obj)
        {
            throw new NotImplementedException();
        }
        
    }
}
