using APIGestionNotas.Domain;

namespace APIGestionNotas.Managers
{
    public class ListaManager : IListaManager
    {
        private readonly List<Lista> _listas;
        private int idCounter = 0;
        public ListaManager()
        {
            _listas = new List<Lista>();
        }

        // Obtener todas las notas[GetAll]

        public List<ListaDTO> GetAll()
        {
            List<ListaDTO> listasDTO = new List<ListaDTO>();
            foreach (Lista lista in _listas)
            {
                listasDTO.Add(ListaMapper.ToDTO(lista));
            }
            return listasDTO;
        }

        //  Obtener nota por id[GetById(int id)]
        public ListaDTO GetById(int id)
        {
            foreach (Lista lista in _listas)
            {
                if (lista.Id == id)
                    return ListaMapper.ToDTO(lista);
            }
            return null;
        }

        //  Crear una nota[Create]
        private int IdGenerator()
        {
            idCounter++;
            return idCounter;
        }
        public ListaDTO Create(ListaDTO listaDto)
        {
            var listaEntidad = ListaMapper.ToEntity(listaDto);

            listaEntidad.Id = IdGenerator();
            listaEntidad.FechaCreacion = DateTime.Now;
            listaEntidad.FechaModificacion = listaEntidad.FechaCreacion;

            _listas.Add(listaEntidad);

            return ListaMapper.ToDTO(listaEntidad);

        }

        //  Actualizar una nota[Update]

        // Convendría crear un DTO por método? por ej. UpdateDTO solo con titulo y contenido
        // TODO: Considerar agregar un parámetro en ToEntity llamado listaExistente que evite hacer new en caso de que la misma sea true
        public ListaDTO Update(ListaDTO listaDTO)
        {
            Lista listaEncontrada = null;
            foreach (Lista lista in _listas)
            {
                if (lista.Id == listaDTO.Id)
                    listaEncontrada = lista;
            }

            if (listaEncontrada == null)
                throw new KeyNotFoundException("Lista no encontrada");

            listaEncontrada.Titulo = listaDTO.Titulo;
            listaEncontrada.Contenido = listaDTO.Contenido;

            listaEncontrada.FechaModificacion = DateTime.Now;

            return ListaMapper.ToDTO(listaEncontrada);

        }

        //  Eliminar una nota[Delete]
        public void Delete(ListaDTO listaDTO)
        {
            Lista listaEncontrada = null;
            foreach (Lista lista in _listas)
            {
                if (lista.Id == listaDTO.Id)
                    listaEncontrada = lista;
            }

            if (listaEncontrada == null)
                throw new KeyNotFoundException("Lista no encontrada");

            _listas.Remove(listaEncontrada);
        }

    }
}
