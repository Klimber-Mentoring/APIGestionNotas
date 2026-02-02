namespace APIGestionNotas.Domain
{
    public class ListaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificación { get; set; }
    }
}
