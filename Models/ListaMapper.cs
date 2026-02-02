namespace APIGestionNotas.Domain
{
    public static class ListaMapper
    {

        public static ListaDTO ToDTO(Lista lista)
        {
            return new ListaDTO
            {
                Id = lista.Id,
                Titulo = lista.Titulo,
                Contenido= lista.Contenido,
                FechaCreacion = lista.FechaCreacion,
                FechaModificación = lista.FechaModificacion
            };
         }
        public static Lista ToEntity(ListaDTO listaDTO)
        {
            return new Lista(listaDTO.Id, listaDTO.Titulo, listaDTO.Contenido, listaDTO.FechaCreacion, listaDTO.FechaModificación);            
        }

    }
}
