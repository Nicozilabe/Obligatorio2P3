using WebMVC.Interfaces;

namespace WebMVC.DTOs.Usuarios
{
    public class DatosUsuariosDTO : IValidable
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

        public DatosUsuariosDTO(string token, string email, string rol)
        {
            Token = token;
            Email = email;
            Rol = rol;
        }
        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
