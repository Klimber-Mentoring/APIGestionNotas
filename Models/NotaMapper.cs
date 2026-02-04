namespace APIGestionNotas.Models
{
    public static class NotaMapper
    {

        public static NotaDTO ToDTO(Nota nota)
        {
            return new NotaDTO(nota.Id, nota.Titulo, nota.Contenido, nota.FechaCreacion, nota.FechaModificacion);
        }

        public static Nota ToEntity(NotaDTO notaDTO)
        {
            return new Nota(notaDTO.Id, notaDTO.Titulo, notaDTO.Contenido, notaDTO.FechaCreacion, notaDTO.FechaModificacion);            
        }

    }
}
