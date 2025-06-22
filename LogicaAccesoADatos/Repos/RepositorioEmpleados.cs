using ExcepcionesPropias;
using LogicaAccesoADatos.EF;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LogicaAccesoADatos.Repos
{
    public class RepositorioEmpleados: IRepositorioEmpleados
    {
        public EmpresaContext Context { get; set; }

        public RepositorioEmpleados(EmpresaContext context)
        {

            Context = context;

        }

        public void Add(Empleado obj)
        {
            if (obj == null)
            {
                throw new DatosInvalidosException("Usuario no válido para el alta.");
            }
            obj.Validar();

            Empleado buscado = FindByEmail(obj.Email.Email);

            if (buscado != null)
            {
                throw new DatosInvalidosException("El Usuario ya existe");
            }
            Context.Empleados.Add(obj);
            Context.SaveChanges();
        }

        public void Remove(int id)
        {
            Empleado aBorrar = FindById(id);
            if (aBorrar == null)
            {
                throw new DatosInvalidosException("No hay un Empleado para borrar aqui");
            }
            bool hayAccion = Context.AccionesAdministracion.Any(a => a.RealizadorId == id);

            if(hayAccion)
            {
                throw new OperacionConflictivaExeption("No se puede eliminar el empleado porque tiene acciones asociadas.");
            }

            Context.Empleados.Remove(aBorrar);
            Context.SaveChanges();
        }

        public void Update(Empleado obj)
        {
            if(obj == null)
            {
                throw new DatosInvalidosException("");
            }
            Empleado aEditar = FindById(obj.Id);
            if (obj.Password == null)
            {
                obj.Password = aEditar.Password;
            }
            obj.Validar();
            
            

            if (aEditar.Nombre.Nombre != obj.Nombre.Nombre || aEditar.Activo != obj.Activo || aEditar.Email.Email != obj.Email.Email || aEditar.Password != obj.Password)
            {
                if(aEditar.Email.Email != obj.Email.Email)
                {
                    Empleado buscado = FindByEmail(obj.Email.Email);
                    if(buscado != null)
                    {
                        throw new DatosInvalidosException("El Email a editar ya existe.");
                    }
                }
            }
            else
            {
                throw new DatosInvalidosException("No se han realizado cambios en el empleado.");
            }
            
            Context.Entry(aEditar).State = EntityState.Detached;
            Context.Empleados.Update(obj); 
            Context.SaveChanges();
        }

        public Empleado FindById(int id)
        {
            if (id <= 0)
            {
                throw new DatosInvalidosException("El id no puede ser menor o igual a cero.");
            }
            Empleado buscado = null;
            try
            {
                buscado = Context.Empleados.Where(Empleado => Empleado.Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new DatosInvalidosException("No se encuentra empleado con el id indicado.");
            }

            return buscado;
        }

        public List<Empleado> FindAll()
        {
            return Context.Empleados.ToList();
        }

        public Empleado? FindByEmail(string email)
        {
            Empleado? buscado = Context.Empleados.AsEnumerable().Where(Empleado => Empleado.Email.Email == email).SingleOrDefault();
            return buscado;
        }
        public Administrador AdministradorPorID(int id)
        {
            if (id <= 0)
            {
                throw new DatosInvalidosException("El id no puede ser menor o igual a cero.");
            }
            Administrador a = null;
            try
            {
                a = Context.Empleados.OfType<Administrador>().SingleOrDefault(a => a.Id == id);
            }
            catch (Exception ex) 
            {
                throw new DatosInvalidosException("No se encontro administrador con el id indicado.");
            }
            return a;
        }

        //public Empleado EmpleadoPorID(int id)
        //{
        //    if (id <= 0)
        //    {
        //        throw new DatosInvalidosException("El id no puede ser menor o igual a cero.");
        //    }
        //    Empleado ret = null;
        //    try
        //    {
        //        ret = Context.Empleados.OfType<Empleado>().SingleOrDefault(a => a.Id == id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new PermisosException("No se encuentra empleado con el id indicado.");
        //    }
        //    return ret;
        //}
    }
}
