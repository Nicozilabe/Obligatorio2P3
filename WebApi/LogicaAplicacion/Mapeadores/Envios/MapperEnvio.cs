using CasosDeUso.DTOs.Envio;
using CasosDeUso.DTOs.Usuarios;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Usuarios;
using LogicaNegocio.EntidadesDominio.Envíos;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.Enums;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mapeadores.Envios
{
    public class MapperEnvio
    {
        public static EnvioComun RegistroDTOToEnvioComun(RegistroEnvioDTO dto, Empleado EmpleadoResponsable, Agencia agencia)
        {
            EnvioComun ret = new EnvioComun();
            if (dto == null)
            {
                throw new DatosInvalidosException("El DTO de registro de envío no puede ser nulo");
            }

            ret.EstadoEnvio = TipoEstadoEnvio.En_Proceso;
            ret.EmpleadoResponable = EmpleadoResponsable;
            ret.Cliente = dto.EmailCliente;
            ret.Peso = dto.Peso;

            ret.Agencia = agencia;
            return ret;
        }
        public static EnvioUrgente RegistroDTOToEnvioUrgente(RegistroEnvioDTO dto, Empleado EmpleadoResponsable, DireccionPostal di, Ciudad c)
        {
            EnvioUrgente ret = new EnvioUrgente();
            if (dto == null)
            {
                throw new DatosInvalidosException("El DTO de registro de envío no puede ser nulo");
            }

            ret.EstadoEnvio = TipoEstadoEnvio.En_Proceso;
            ret.EmpleadoResponable = EmpleadoResponsable;
            ret.Cliente = dto.EmailCliente;
            ret.Peso = dto.Peso;
            ret.Ciudad = c;
            ret.Direccion = di;
            return ret;
        }

        public static EnvioDTO ToDTO(Envio e)
        {
            EnvioDTO ret = new EnvioDTO();

            ret.Id = e.Id;
            ret.Peso = e.Peso;
            ret.EmailCliente = e.Cliente;
            ret.EstadoEnvio = e.EstadoEnvio.ToString();
            ret.TipoEnvio = e.GetType().Name;
            ret.FechaRegistroEnvio = e.FechaRegistroEnvio;
            if (e is EnvioComun)
            {
                EnvioComun ec = (EnvioComun)e;
                ret.Agencia = MapperAgencia.ToDTO(ec.Agencia);
            }
            else if (e is EnvioUrgente)
            {
                EnvioUrgente eu = (EnvioUrgente)e;
                ret.direccion = MapperDireccion.ToDTO(eu.Direccion);
                ret.Ciudad = MapperCiudad.ToDTO(eu.Ciudad);
                ret.EnvioEficiente = eu.EnvioEficiente;
            }
            ret.EmpleadoResponable = MappersEmpleado.ToEmpleadoSeguroDTO(e.EmpleadoResponable);
            ret.Comentarios = MapperComentarioEnvio.ToListDTO(e.Comentarios);
            ret.Tracking = e.Tracking;
            ret.FechaEntrega = e.FechaEntrega;

            return ret;
        }

   
        public static EnvioLigthDTO ToEnvioLigthDTO(Envio e)
        {
            EnvioLigthDTO ret = new EnvioLigthDTO();

            ret.Id = e.Id;
            ret.Peso = e.Peso;
            ret.EmailCliente = e.Cliente;
            ret.EstadoEnvio = e.EstadoEnvio.ToString();
            ret.TipoEnvio = e.GetType().Name;
            ret.FechaRegistroEnvio = e.FechaRegistroEnvio;
            ret.Tracking = e.Tracking;
            if (e is EnvioComun)
            {
                EnvioComun ec = (EnvioComun)e;
                if (ec.Agencia != null)
                {
                ret.Destino = ec.Agencia.ToString();
                }
                else
                {
                    ret.Destino = "No se pudo obtener el destino.";  
                }
                      
            }
            else if (e is EnvioUrgente)
            {
                EnvioUrgente eu = (EnvioUrgente)e;
                if (eu.Direccion != null && eu.Ciudad != null)
                {
                    string direccion = eu.Direccion.ToString() + " " + eu.Ciudad.ToString();
                    ret.Destino = direccion;
                }
                else
                {
                    ret.Destino = "No se pudo obtener el destino.";
                }
                
            }
            ret.Empleado = MappersEmpleado.ToEmpleadoDTO(e.EmpleadoResponable);
            return ret;
        }

        public static IEnumerable<EnvioLigthDTO> ToListEnvioLigthDTO(IEnumerable<Envio> envios)
        {
            List<EnvioLigthDTO> DTOs = new List<EnvioLigthDTO>();
            foreach (Envio e in envios)
            {
                EnvioLigthDTO dto = ToEnvioLigthDTO(e);
                DTOs.Add(dto);
            }
            return DTOs;
        }


    }
}
