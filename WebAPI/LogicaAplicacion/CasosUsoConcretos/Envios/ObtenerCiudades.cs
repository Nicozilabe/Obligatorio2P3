using CasosDeUso.DTOs.Envio;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Envios;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUsoConcretos.Envios
{
    public class ObtenerCiudades: IObtenerCiudades
    {
        IRepositorioCiudades repo { get; set; }

        public ObtenerCiudades(IRepositorioCiudades repo)
        {
            this.repo = repo;
        }
        public IEnumerable<CiudadDTO> GetCiudades()
        {
            IEnumerable<CiudadDTO> ciudades = MapperCiudad.ToListDTO(repo.FindAll());

            if (ciudades == null || ciudades.Count() == 0)
            {
                throw new DatosInvalidosException("No se encontraron ciudades");
            }

            return ciudades;
        }
    }

}
