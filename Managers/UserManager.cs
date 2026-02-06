using APIGestionNotas.Enums;
using APIGestionNotas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace APIGestionNotas.Managers
{
    public class UserManager : IUserManager
    {
        public List<Usuario> Usuarios { get; set; }
        private readonly IMapper _mapper;

        public UserManager(IMapper mapper)
        {
            Usuarios = new List<Usuario>();
            _mapper = mapper;
        }

        public UserDTO Create(UserCreateDTO userDTO)
        {
            var usuarioEntidad = _mapper.Map<Usuario>(userDTO);
            
            var hash = new PasswordHasher<Usuario>();
            usuarioEntidad.PasswordHash = hash.HashPassword(usuarioEntidad, userDTO.Password);

            Usuarios.Add(usuarioEntidad);

            return _mapper.Map<UserDTO>(usuarioEntidad);

        }

        public Usuario ObtenerUsuario(string username)
        {
            foreach (Usuario usuario in Usuarios)
            {
                if (usuario.UserName == username)
                {
                    return usuario;
                }
            }
            return null;
        }

        public bool ValidarPassword(Usuario usuario, string passwordIngresado)
        {
            var hash = new PasswordHasher<Usuario>();
            var passwordValida = hash.VerifyHashedPassword(usuario, usuario.PasswordHash, passwordIngresado);
            return (passwordValida == PasswordVerificationResult.Success);
        }


        public string RolAString(TipoDeUsuario Rol)
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
        
        public TipoDeUsuario StringARol(string rol)
        {
            switch (rol.Trim().ToUpper())
            {
                case ("ADMIN"):
                    return TipoDeUsuario.ADMIN;
                    break;
                case ("USER"):
                    return TipoDeUsuario.USUARIO;
                    break;
            }
            return TipoDeUsuario.USUARIO;
        }
    }
}
