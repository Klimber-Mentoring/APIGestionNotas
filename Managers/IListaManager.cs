using APIGestionNotas.Domain;

namespace APIGestionNotas.Managers
{
    public interface IListaManager
    {
        List<ListaDTO> GetAll();
        ListaDTO GetById(int id);
        ListaDTO Create(ListaDTO listaDto);
        ListaDTO Update(ListaDTO listaDTO);
        void Delete(ListaDTO listaDTO);
    }
}
