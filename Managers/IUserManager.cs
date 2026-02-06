using APIGestionNotas.Enums;
using APIGestionNotas.Models;

namespace APIGestionNotas.Managers
{
    public interface IUserManager
    {
        Usuario ObtenerUsuario(string username);

        UserDTO Create(UserCreateDTO userDTO);

        bool ValidarPassword(Usuario usuario, string passwordIngresado);

        string RolAString(TipoDeUsuario Rol);
        TipoDeUsuario StringARol(string rol);
    }
}
