using APIGestionNotas.Enums;
using Microsoft.AspNetCore.Identity;

namespace APIGestionNotas.Models
{
    public class Usuario : IdentityUser
    {
        public TipoDeUsuario Rol { get; set; }
        public string password;

        public Usuario(string username, string password, TipoDeUsuario rol)
        {
            this.UserName = username;
            this.password = password;
            this.Rol = rol;
        }

        public string ObtenerRol(TipoDeUsuario Rol)
        {
            switch (Rol)
            {
                case (TipoDeUsuario.ADMIN):
                    return "Admin";
                    break;
                case (TipoDeUsuario.USUARIO):
                    return "User";
                    break;
            }
            return null;
        }
    }
}
