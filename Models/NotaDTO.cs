namespace APIGestionNotas.Domain
{
    public class NotaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }


        public NotaDTO(int id, string titulo, string? contenido, DateTime fechaCreacion, DateTime fechaModificacion)
        {
            Id = id;
            Titulo = titulo;
            Contenido = contenido;
            FechaCreacion = fechaCreacion;
            FechaModificacion = fechaModificacion;
        }

    }
}
