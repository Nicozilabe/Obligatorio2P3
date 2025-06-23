using ExcepcionesPropias;
using LogicaAccesoADatos.EF;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.ValueObjects.Usuario;
using Microsoft.EntityFrameworkCore;
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
                throw new DatosInvalidosException("No hay un Usuario para borrar aqui");
            }
            Context.Usuarios.Remove(aBorrar);
            Context.SaveChanges();
        }

        public void Update(Usuario obj)
        {
            if (obj == null)
            {
                throw new DatosInvalidosException("");
            }
            Usuario aEditar = FindById(obj.Id);
            if (obj.Password == null)
            {
                obj.Password = aEditar.Password;
            }
            obj.Validar();



            if (aEditar.Nombre.Nombre != obj.Nombre.Nombre || aEditar.Activo != obj.Activo || aEditar.Email.Email != obj.Email.Email || aEditar.Password != obj.Password)
            {
                if (aEditar.Email.Email != obj.Email.Email)
                {
                    Usuario buscado = FindByEmail(obj.Email.Email);
                    if (buscado != null)
                    {
                        throw new DatosInvalidosException("El Email a editar ya existe.");
                    }
                }
            }
            else
            {
                throw new DatosInvalidosException("No se han realizado cambios en el Usuario.");
            }

            Context.Entry(aEditar).State = EntityState.Detached;
            Context.Usuarios.Update(obj);
            Context.SaveChanges();
        }
        
    }
}
