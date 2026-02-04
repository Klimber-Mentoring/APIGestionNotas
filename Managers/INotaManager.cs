using APIGestionNotas.Models;

namespace APIGestionNotas.Managers
{
    public interface INotaManager
    {
        List<NotaDTO> GetAll();
        NotaDTO GetById(Guid id);
        NotaDTO Create(NotaDTO notaDTO);
        NotaDTO Update(Guid id, UpdateNotaDTO updateNotaDTO);
        void Delete(NotaDTO notaDTO);
    }
}