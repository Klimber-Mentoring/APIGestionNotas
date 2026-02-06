using APIGestionNotas.Enums;

namespace APIGestionNotas.Models
{
    public class UserCreateDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public TipoDeUsuario Rol { get; set; }

        public UserCreateDTO(string username, string password, TipoDeUsuario rol)
        {
            Username = username;
            Password = password;
            Rol = rol;
        }
    }
}
