using CasosDeUso.DTOs.Usuarios;
using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using LogicaAplicacion.Mapeadores.Usuarios;
using LogicaNegocio.EntidadesDominio.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUsoConcretos.Usuarios
{
    public class Login : ILogin
    {
        public IRepositorioUsuarios Repo { get; set; }

        public Login(IRepositorioUsuarios repo) { Repo = repo; }

        public UsuarioDTO? RealizarLogin(LoginDTO datos)
        {
            UsuarioDTO? ret = null;
            Usuario buscado = Repo.FindByEmail(datos.Email);
            if (buscado != null)
            {
                if (buscado.Password.Password == datos.Pass)
                {
                    if (buscado is Empleado || buscado is Administrador)
                    {
                        ret = MappersUsuario.ToUsuarioDTO(buscado);
                    }
                    else
                    {
                        throw new PermisosException("El usuario no cuenta con los permisos para iniciar sesión.");
                    }

                }

            }
            if (ret == null)
            {
                throw new DatosInvalidosException("Email o Contraseña no válidos");
            }

            return ret;
        }

    }
}
