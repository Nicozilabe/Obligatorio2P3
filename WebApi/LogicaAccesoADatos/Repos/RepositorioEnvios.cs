using ExcepcionesPropias;
using LogicaAccesoADatos.EF;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.Enums;
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
    public class RepositorioEnvios : IRepositorioEnvios
    {

        public EmpresaContext Context { get; set; }

        public RepositorioEnvios(EmpresaContext context)
        {
            Context = context;
        }

        public void Add(Envio obj)
        {
            if (obj == null)
            {
                throw new DatosInvalidosException("Envío no válido para el alta.");
            }
            obj.Tracking=(GetLastTracking()+1);
            obj.FechaRegistroEnvio = DateTime.Now;
            //Validamos el Envío acá porque es el que genera el tracking(Para que no se desface la variable o se pierda)
            obj.Validar();
            Context.Envios.Add(obj);
            Context.SaveChanges();
        }
        public int GetLastTracking()
        {
            var lastTracking = Context.Envios.OrderByDescending(e => e.Tracking).FirstOrDefault();
            if (lastTracking != null)
            {
                return lastTracking.Tracking;
            }
            return 0; 
        }
        public List<Envio> FindAll()
        {
            throw new NotImplementedException();
        }

        public Envio FindById(int id)
        {
            Envio buscado = Context.Envios.Where(e => e.Id == id).SingleOrDefault();
            EnvioUrgente urgente = null;
            EnvioComun comun = null;


            if ( buscado is EnvioUrgente)
            {
                 urgente = Context.Envios.OfType<EnvioUrgente>().Where(e => e.Id == id).Include(a => a.Cliente).Include(e => e.Ciudad).Include(e => e.Direccion).Include(e => e.EmpleadoResponable).Include(e => e.Comentarios).SingleOrDefault();
            }
            if (buscado is EnvioComun)
            {
                 comun = Context.Envios.OfType<EnvioComun>().Where(e => e.Id == id)
                    .Include(e => e.Agencia).Include(e => e.Agencia.Direccion).Include(a => a.Cliente).Include(e => e.EmpleadoResponable)
                    .Include(e => e.Agencia.Ubicacion).Include(e => e.Agencia.Ciudad).Include(a => a.Cliente).Include(e => e.Comentarios).SingleOrDefault();
            }

            if (urgente != null)
            {
                return urgente;
            }
            else if (comun != null)
            {
                return comun;
            }
            else
            {
                return buscado;
            }

        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Envio obj)
        {
            if (obj == null)
            {
                throw new DatosInvalidosException("Error al registrar cambio envio.");
            }
            obj.Validar();
            Envio aEditar = FindById(obj.Id);

            //if (aEditar.FechaEntrega == obj.FechaEntrega && aEditar.EstadoEnvio == obj.EstadoEnvio && aEditar.FechaRegistroEnvio == obj.FechaRegistroEnvio &&
            //    aEditar.Comentarios == obj.Comentarios)
            //{
            //    //agregar más validaciones si se necesita
            //    throw new DatosInvalidosException("No se han ingresado cambios al envío.");                
            //}
            Context.Entry(aEditar).State = EntityState.Detached;
            Context.Envios.Update(obj);
            Context.SaveChanges();
        }

        public IEnumerable<Envio> FindAllLightActivos()
        {
            List<Envio> ret = new List<Envio>();
            ret.AddRange(Context.EnviosComunes.Include(a => a.Agencia).Include(a => a.Cliente).Where(a => a.EstadoEnvio == TipoEstadoEnvio.En_Proceso).ToList());
            ret.AddRange(Context.EnviosUrgentes.Include(a => a.Direccion).Include(a => a.Ciudad).Include(a => a.Cliente).Where(a => a.EstadoEnvio == TipoEstadoEnvio.En_Proceso).ToList());
            return ret;
        }

        public Envio FindByTracking(int tracking)
        {
            Envio buscado = Context.Envios.Where(e => e.Tracking == tracking).SingleOrDefault();
            EnvioUrgente urgente = null;
            EnvioComun comun = null;


            if (buscado is EnvioUrgente)
            {
                urgente = Context.Envios.OfType<EnvioUrgente>().Where(e => e.Tracking == tracking).Include(e => e.Ciudad).Include(e => e.Direccion).Include(e => e.EmpleadoResponable).Include(e => e.Comentarios).Include(a => a.Cliente).SingleOrDefault();
            }
            if (buscado is EnvioComun)
            {
                comun = Context.Envios.OfType<EnvioComun>().Where(e => e.Tracking == tracking)
                   .Include(e => e.Agencia).Include(e => e.Agencia.Direccion).Include(e => e.EmpleadoResponable)
                   .Include(e => e.Agencia.Ubicacion).Include(e => e.Agencia.Ciudad).Include(e => e.Comentarios).Include(a => a.Cliente).SingleOrDefault();
            }

            if (urgente != null)
            {
                return urgente;
            }
            else if (comun != null)
            {
                return comun;
            }
            else
            {
                return buscado;
            }
        }

        public IEnumerable<Envio> FindAllLightByEmailCliente(string eCliente)
        {
            List<Envio> ret = new List<Envio>();
            ret.AddRange(Context.EnviosComunes.Include(a => a.Agencia).Include(a => a.Cliente).Where(a => a.Cliente.Email == new UsuarioEmail(eCliente)).ToList());
            ret.AddRange(Context.EnviosUrgentes.Include(a => a.Direccion).Include(a => a.Cliente).Include(a => a.Ciudad).Where(a => a.Cliente.Email == new UsuarioEmail(eCliente)).ToList());
            return ret;
        }

        public IEnumerable<Envio> FindAllLightByComentario(string eCliente, string comentario)
        {
            List<Envio> ret = new List<Envio>();
            ret.AddRange(Context.EnviosComunes.Include(a => a.Agencia).Include(a => a.Cliente).Where(a => a.Cliente.Email == new UsuarioEmail(eCliente) && a.Comentarios != null && a.Comentarios.Any(c => c.Comentario.ToLower().Contains(comentario.ToLower()))).ToList());
            ret.AddRange(Context.EnviosUrgentes.Include(a => a.Direccion).Include(a => a.Ciudad).Include(a => a.Cliente).Where(a => a.Cliente.Email == new UsuarioEmail(eCliente) && a.Comentarios != null && a.Comentarios.Any(c => c.Comentario.ToLower().Contains(comentario.ToLower()))).ToList());
            return ret;
        }
    }
}
