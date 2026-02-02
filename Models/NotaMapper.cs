namespace APIGestionNotas.Domain
{
    public static class NotaMapper
    {

        public static NotaDTO ToDTO(Nota nota)
        {
            return new NotaDTO
            {
                Id = nota.Id,
                Titulo = nota.Titulo,
                Contenido= nota.Contenido,
                FechaCreacion = nota.FechaCreacion,
                FechaModificación = nota.FechaModificacion
            };
         }
        public static Nota ToEntity(NotaDTO notaDTO)
        {
            return new Nota(notaDTO.Id, notaDTO.Titulo, notaDTO.Contenido, notaDTO.FechaCreacion, notaDTO.FechaModificación);            
        }

    }
}
