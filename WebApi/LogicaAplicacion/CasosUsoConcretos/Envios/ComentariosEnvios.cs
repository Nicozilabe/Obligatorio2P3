using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Envios;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUsoConcretos.Envios
{
    public class ComentariosEnvios : IComentarioEnvio
    {

        IRepositorioEnvios repoEnvios { get; set; }

        IRepositorioEmpleados repoEmpleados { get; set; }
        IRepositorioComentarios repoComentarios { get; set; }


        public ComentariosEnvios(IRepositorioEnvios repoEnvios, IRepositorioEmpleados repoEmpleados, IRepositorioComentarios repoComentarios)
        {
            this.repoEnvios = repoEnvios;
            this.repoEmpleados = repoEmpleados;
            this.repoComentarios = repoComentarios;
        }


        public void AgregarComentario(int envioId, ComentarioEnvioDTO comentario)
        {
            comentario.Validar();
            if (envioId <= 0)
            {
                throw new DatosInvalidosException("El ID del envío debe ser mayor a cero.");
            }

            ComentarioEnvio c = MapperComentarioEnvio.ToComentario(comentario);

            if (comentario.EmpleadoId != null)
            {
                try
                {
                    Empleado e = repoEmpleados.FindById((int)comentario.EmpleadoId);
                    if (e == null)
                    {
                        throw new DatosInvalidosException("El empleado no existe");
                    }
                    else
                    {
                        c.Empleado = e;
                    }

                }
                catch (Exception ex)
                {
                    throw new DatosInvalidosException("El empleado no existe", ex);
                }
            }

            comentario.Validar();

            Envio envio = repoEnvios.FindById(envioId);

            if (envio == null)
            {
                throw new DatosInvalidosException("El envío no existe");
            }
            else
            {
                c.Envio = envio;
            }

            repoComentarios.Add(c);


        }


    }
}
