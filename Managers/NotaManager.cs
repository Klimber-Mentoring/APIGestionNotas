using APIGestionNotas.Models;
using AutoMapper;


namespace APIGestionNotas.Managers
{
    public class NotaManager : INotaManager
    {
        private readonly IMapper _mapper;
        public List<Nota> Notas { get; set; }
        public NotaManager(IMapper mapper)
        {
            _mapper = mapper;
            Notas = new List<Nota>();
        }

        // Obtener todas las notas[GetAll]

        public List<NotaDTO> GetAll()
        {
            List<NotaDTO> notasDTO = new List<NotaDTO>();

            foreach (Nota nota in Notas)
            {
                notasDTO.Add(_mapper.Map<NotaDTO>(nota));
            }
            return (notasDTO.OrderBy(x => x.FechaCreacion).ToList());
        }

        //  Obtener nota por id[GetById(Guid id)]
        public NotaDTO GetById(Guid id)
        {
            foreach (Nota nota in Notas)
            {
                if (nota.Id == id)
                    return _mapper.Map<NotaDTO>(nota);
            }
            return null;
        }

        //  Crear una nota[Create]
        
        public NotaDTO Create(NotaDTO notaDTO)
        {
            var notaEntidad = _mapper.Map<Nota>(notaDTO);

            notaEntidad.FechaCreacion = DateTime.Now;
            notaEntidad.FechaModificacion = notaEntidad.FechaCreacion;

            Notas.Add(notaEntidad);

            return _mapper.Map<NotaDTO>(notaEntidad);

        }

        //  Actualizar una nota[Update]
        public NotaDTO Update(Guid id, UpdateNotaDTO updateNotaDTO)
        {
            Nota notaEncontrada = null;
            foreach (Nota nota in Notas)
            {
                if (nota.Id == id)
                    notaEncontrada = nota;
            }

            if (notaEncontrada == null)
                return null;

            notaEncontrada.Titulo = updateNotaDTO.Titulo;
            notaEncontrada.Contenido = updateNotaDTO.Contenido;

            notaEncontrada.FechaModificacion = DateTime.Now;

            return _mapper.Map<NotaDTO>(notaEncontrada);

        }

        //  Eliminar una nota[Delete]dot
        public void Delete(NotaDTO notaDTO)
        {
            Nota notaEncontrada = null;
            foreach (Nota nota in Notas)
            {
                if (nota.Id == notaDTO.Id)
                    notaEncontrada = nota;
            }
            Notas.Remove(notaEncontrada);
        }

    }
}
