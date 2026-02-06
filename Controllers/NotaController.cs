using APIGestionNotas.Enums;
using APIGestionNotas.Managers;
using APIGestionNotas.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace APIGestionNotas.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class NotaController: ControllerBase
    {
        private readonly INotaManager _notaManager;
        private readonly IUserManager _userManager;
        public NotaController(INotaManager notaManager, IUserManager userManager)
        {
            _notaManager = notaManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Devuelve todas las notas disponibles ordenadas según su fecha de creación.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Devuelve la lista de notas</response>

        [Authorize(Roles = ("Admin, User"))]
        [HttpGet]
        public ActionResult<List<NotaDTO>> GetAll()
        {
            return _notaManager.GetAll();

        }

        /// <summary>
        /// Devuelve una nota específica en base a su su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de una nota</param>
        /// <returns>La nota solicitada</returns>
        /// <response code="200">Devuelve la nota encontrada</response>
        /// <response code="400">No se encontró una nota con el id especificado</response>

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NotaDTO> Get(Guid id)
        {
            var notaDTO = _notaManager.GetById(id);

            if (notaDTO == null)
                return NotFound();

            return notaDTO;
        }

        /// <summary>
        /// Crea una nueva nota.
        /// </summary>
        /// <param name="nota">Datos de la nota que se quiere crear</param>
        /// <returns>La nota creada</returns>
        /// <response code="201">La nota fue creada exitosamente</response>
        /// <response code="400">Datos inválidos en la solicitud</response>
        /// 
        [Authorize(Roles = "Admin, User")]

        [HttpPost("nota")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(NotaDTO nota)
        {
            var notaCreada = _notaManager.Create(nota);

            return Created("", new { id = notaCreada.Id });
        }

        
        /// <summary>
        /// Actualiza una nota existente.
        /// </summary>
        /// <param name="id">Identificador de la nota a actualizar</param>
        /// <param name="nota">Datos actualizados de la nota</param>
        /// <response code="204">La nota se actualizó correctamente</response>
        /// <response code="404">No se encontró la nota a actualizar</response>

        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NotaDTO> Update(Guid id, UpdateNotaDTO nota)
        {
            var existingnota = _notaManager.GetById(id);
            if (existingnota is null)
                return NotFound();

            return _notaManager.Update(id, nota);
        }

        /// <summary>
        /// Elimina una nota.
        /// </summary>
        /// <param name="id">Identificador de la nota a eliminar</param>
        /// <response code="204">La nota se eliminó correctamente</response>
        /// <response code="404">No se encontró la nota a eliminar</response>
        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var nota = _notaManager.GetById(id);

            if (nota is null)
                return NotFound();

            _notaManager.Delete(nota);

            return NoContent();
        }


        /// <summary>
        /// Devuelve user|pass encriptados de un usuario si este existe
        /// </summary>
        /// <param name="username">Username del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>user|pass encriptados</returns>
        /// ///<response code="204">La operación se realizó con éxito</response>
        /// ///<response code="404">No se encontró un usuario con esas credenciales</response>
       
        [AllowAnonymous]
        [HttpPost("usuarioLogin")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            var usuario = _userManager.ObtenerUsuario(username);

            if ((usuario == null) || !_userManager.ValidarPassword(usuario, password))
            {
                return Unauthorized();
            }

            var patron = $"{username}:{password}";
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(patron));

            return Ok(new { authorization = $"Basic {base64}"});
        }


    }
}
