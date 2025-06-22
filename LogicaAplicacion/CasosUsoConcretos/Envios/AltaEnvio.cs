using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Envios;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUsoConcretos.Envios
{
    public class AltaEnvio : IAltaEnvio
    {
        IRepositorioEnvios repo { get; set; }
        IRepositorioEmpleados repoEmpleados { get; set; }
        IRepositorioAgencias repoAgencias { get; set; }
        IRepositorioCiudades repoCiudades { get; set; }

        public AltaEnvio(IRepositorioEnvios repo, IRepositorioEmpleados repositorioEmpleados,IRepositorioAgencias repositorioAgencias, IRepositorioCiudades repoCiudades)
        {
            this.repo = repo;
            repoEmpleados = repositorioEmpleados;
            repoAgencias = repositorioAgencias;
            this.repoCiudades = repoCiudades;
        }
        public void RegistroEnvio(RegistroEnvioDTO envio)
        {   
            envio.Validar();
            Empleado responsable = repoEmpleados.FindById((int)envio.IdEmpleadoResponable);
            if (responsable == null)
            {
                throw new PermisosException("La acción debe ser ralizada por un empleado");
            }
            
            if(envio.TipoEnvio == "C")
            {
                Agencia a = repoAgencias.FindById((int)envio.IdAgencia);
                EnvioComun e = MapperEnvio.RegistroDTOToEnvioComun(envio, responsable, a);
                repo.Add(e);
            }
            else if (envio.TipoEnvio == "U")
            {
                Ciudad c = repoCiudades.FindById((int)envio.IdCiudad);
                DireccionPostal di = MapperDireccion.ToDireccion(envio.direccion);
                EnvioUrgente e = MapperEnvio.RegistroDTOToEnvioUrgente(envio, responsable, di, c);
                repo.Add(e);
            }
            else
            {
                throw new Exception("Tipo de envío no válido");
            }
        }
    }
}


