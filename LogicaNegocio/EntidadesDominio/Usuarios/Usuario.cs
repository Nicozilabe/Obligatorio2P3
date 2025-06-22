using ExcepcionesPropias;
using LogicaNegocio.ValueObjects.Usuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio.Usuarios
{
    [Index(nameof(Email.Email), IsUnique = true)]
    public abstract class Usuario
    {
        public int Id { get; set; }
        
        public UsuarioNombre Nombre { get; set; }

        public UsuarioEmail Email { get; set; }
        public UsuarioPassword Password { get; set; }
        public bool Activo { get; set; }

        public Usuario() { 
            Activo = true;
        }

        public Usuario(UsuarioNombre nombre, UsuarioEmail email, UsuarioPassword password) { 
            Nombre = nombre;
            Email = email;
            Password = password;
            Activo = true;
            Validar();
            
        }
        public virtual void Validar()
        {
            if (Nombre == null) throw new DatosInvalidosException("El nombre y el apellido son obligatorios de llenar.");
            if(Email == null) throw new DatosInvalidosException("El Email es obligatorio.");
            if (Password == null) throw new DatosInvalidosException("La contraseña es obligatoria.");
        }
    }
}
