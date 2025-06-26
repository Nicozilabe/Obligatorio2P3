﻿using LogicaNegocio.EntidadesDominio.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioUsuarios:IRepositorio<Usuario>
    {
        Usuario FindByEmail(string email);
    }
}