using APIGestionNotas.Models;
using AutoMapper;


namespace APIGestionNotas.Managers
{
    public class NotaManager : INotaManager
    {
        private readonly IMapper _mapper;
        public List<Nota> Notas { get; set; }
        private int idCounter = 0;
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

        //  Obtener nota por id[GetById(int id)]
        public NotaDTO GetById(int id)
        {
            foreach (Nota nota in Notas)
            {
                if (nota.Id == id)
                    return _mapper.Map<NotaDTO>(nota);
            }
            return null;
        }

        //  Crear una nota[Create]
        private int IdGenerator()
        {
            int lista = Notas.Count();
            int idNuevo = (lista > 0) ? lista : idCounter++;

            return idNuevo;
        }
        public NotaDTO Create(NotaDTO notaDTO)
        {
            var notaEntidad = _mapper.Map<Nota>(notaDTO);

            notaEntidad.Id = IdGenerator();
            notaEntidad.FechaCreacion = DateTime.Now;
            notaEntidad.FechaModificacion = notaEntidad.FechaCreacion;

            Notas.Add(notaEntidad);

            return _mapper.Map<NotaDTO>(notaEntidad);

        }

        //  Actualizar una nota[Update]

        // Convendría crear un DTO por método? por ej. UpdateDTO solo con titulo y contenido
        // TODO: Considerar agregar un parámetro en ToEntity llamado notaExistente que evite hacer new en caso de que la misma sea true
        public NotaDTO Update(NotaDTO notaDTO)
        {
            Nota notaEncontrada = null;
            foreach (Nota nota in Notas)
            {
                if (nota.Id == notaDTO.Id)
                    notaEncontrada = nota;
            }

            if (notaEncontrada == null)
                return null;

            notaEncontrada.Titulo = notaDTO.Titulo;
            notaEncontrada.Contenido = notaDTO.Contenido;

            notaEncontrada.FechaModificacion = DateTime.Now;

            return _mapper.Map<NotaDTO>(notaEncontrada);

        }

        //  Eliminar una nota[Delete]
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
