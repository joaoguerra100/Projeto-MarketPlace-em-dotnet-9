using MarketPlace.Models;

namespace MarketPlace.Interface.Repositorie
{
    public interface ICategoriaRepository
    {
        Task<Categoria?> GetByIdAsync(Guid id);
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria?> PesquisaPorNome(string nome);
        Task<PaginacaoResponse<Categoria>> ObterPaginadoAsync(string? termoBusca, int pagina, int tamanhoPagina, bool ordemDesc);
        Task AddAscyn(Categoria categoria);
        void Update(Categoria categoria);
        void Delete(Categoria categoria);
        Task<bool> SaveChangesAsync();
    }
}