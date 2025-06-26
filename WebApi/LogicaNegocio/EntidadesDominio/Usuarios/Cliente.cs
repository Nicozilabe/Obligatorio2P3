using LogicaNegocio.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Usuarios
{
    public class Cliente:Usuario
    {
        public Cliente() { }
        public Cliente(UsuarioNombre nombre, UsuarioEmail email, UsuarioPassword password) : base(nombre, email, password) { }
    }
}