using APIGestionNotas.Domain;

namespace APIGestionNotas.Managers
{
    public interface INotaManager
    {
        List<NotaDTO> GetAll();
        NotaDTO GetById(int id);
        NotaDTO Create(NotaDTO notaDTO);
        NotaDTO Update(NotaDTO notaDTO);
        void Delete(NotaDTO notaDTO);
    }
}