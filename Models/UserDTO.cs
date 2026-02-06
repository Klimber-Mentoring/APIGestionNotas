using APIGestionNotas.Enums;
namespace APIGestionNotas.Models
{
    public class UserDTO
    {
        public string Username { get; set; }
        public TipoDeUsuario Rol {  get; set; }

        public UserDTO(string username,  TipoDeUsuario rol)
        {
            Username = username;
            Rol = rol;
        }
    }
}
