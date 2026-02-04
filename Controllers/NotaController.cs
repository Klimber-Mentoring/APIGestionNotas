using APIGestionNotas.Models;
using APIGestionNotas.Managers;
using Microsoft.AspNetCore.Mvc;

namespace APIGestionNotas.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class NotaController: ControllerBase
    {
        private readonly INotaManager _notaManager;
        public NotaController(INotaManager notaManager)
        {
            _notaManager = notaManager;
        }

        /// <summary>
        /// Devuelve todas las notas disponibles ordenadas según su fecha de creación.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Devuelve la lista de notas</response>
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
        public ActionResult<NotaDTO> Get(int id)
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(NotaDTO nota)
        {
            var notaCreada = _notaManager.Create(nota);

            return CreatedAtAction(nameof(Get), new { id = notaCreada.Id }, notaCreada);
        }


        /// <summary>
        /// Actualiza una nota existente.
        /// </summary>
        /// <param name="id">Identificador de la nota a actualizar (debe coincidir con nota.Id)</param>
        /// <param name="nota">Datos actualizados de la nota</param>
        /// <response code="204">La nota se actualizó correctamente</response>
        /// <response code="400">El id no coincide con el id de la nota proporcionada</response>
        /// <response code="404">No se encontró la nota a actualizar</response>
        
        /// TODO: se podría crear un notaUpdateDTO que incluya solo Título y Contenido para evitar la necesidad de que 
        /// el id de la nota coincida con el DTO, o al menos que el id se pase directamente con el DTO.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, NotaDTO nota)
        {
            if (id != nota.Id)
                return BadRequest();

            var existingnota = _notaManager.GetById(id);
            if (existingnota is null)
                return NotFound();

            _notaManager.Update(nota);
            return NoContent();
        }

        /// <summary>
        /// Elimina una nota.
        /// </summary>
        /// <param name="id">Identificador de la nota a eliminar</param>
        /// <response code="204">La nota se eliminó correctamente</response>
        /// <response code="404">No se encontró la nota a eliminar</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var nota = _notaManager.GetById(id);

            if (nota is null)
                return NotFound();

            _notaManager.Delete(nota);

            return NoContent();
        }

    }
}
