using APIGestionNotas.Domain;
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

        [HttpGet]
        public ActionResult<List<NotaDTO>> GetAll()
        {
            return _notaManager.GetAll();
        }


        [HttpGet("{id}")]
        public ActionResult<NotaDTO> Get(int id)
        {
            var notaDTO = _notaManager.GetById(id);

            if (notaDTO == null)
                return NotFound();

            return notaDTO;
        }

        [HttpPost]
        public IActionResult Create(NotaDTO nota)
        {
            var notaCreada = _notaManager.Create(nota);
            return CreatedAtAction(nameof(Get), new { id = notaCreada.Id }, notaCreada);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
