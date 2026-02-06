using APIGestionNotas.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace APIGestionNotas.Auth
{
    public class NotaAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserManager _userManager;
        public NotaAuthenticationHandler(IUserManager userManager,IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _userManager = userManager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.Fail("Unathorized");
            }

            if (!authorizationHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = authorizationHeader.Substring(6);

            var credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var credentials = credentialAsString.Split(":");
            if (credentials?.Length != 2)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var username = credentials[0];
            var password = credentials[1];

            var usuario = _userManager.ObtenerUsuario(username);

             //repo
            if ((usuario == null) || !_userManager.ValidarPassword(usuario, password))
            {
                return AuthenticateResult.Fail("Authentication failed");
            }
             
           
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, _userManager.RolAString(usuario.Rol))  //repo
            }; //List of attributes for the current user


            var identity = new ClaimsIdentity(claims, "Basic");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
        }
    }
}
