using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Usuarios;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUsoConcretos.Usuarios
{
    public class CambiarContrasena : ICambiarContraseña
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CambiarContrasena(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }

        public void CambiarContraseña(CambioContrasenaDTO datos)
        {
            datos.Validar();
            if (datos.PassVieja == datos.PassNueva)
            {
                throw new DatosInvalidosException("La contraseña nueva no puede ser igual a la anterior.");
            }

            Usuario buscado = Repo.FindByEmail(datos.Email);
            if (buscado != null)
            {
                if (buscado.Password.Password == datos.PassVieja)
                {
                    buscado.Password = new UsuarioPassword(datos.PassNueva);
                    Repo.Update(buscado);
                }
                else
                {
                    throw new DatosInvalidosException("La contraseña actual no es correcta");
                }
            }
            else
            {
                throw new DatosInvalidosException("El usuario no existe.");
            }
        }
    }
}