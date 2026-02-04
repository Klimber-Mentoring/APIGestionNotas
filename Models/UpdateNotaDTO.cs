namespace APIGestionNotas.Models
{
    public class UpdateNotaDTO
    {
        public string Titulo { get; set; }
        public string? Contenido { get; set; }


        public UpdateNotaDTO(string titulo, string? contenido)
        {
            Titulo = titulo;
            Contenido = contenido;

        }

    }
}
