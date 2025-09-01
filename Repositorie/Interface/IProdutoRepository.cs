using MarketPlace.Models;

namespace MarketPlace.Interface.Repositorie
{
    public interface IProdutoRepository
    {
        Task<Produto?> GetProdutoById(Guid id);
        Task<IEnumerable<Produto>> GetAllProduto();
        Task<IEnumerable<Produto>> PesquisaPorProdutoAsync(string valor);
        Task<PaginacaoResponse<Produto>> ObterPaginadoAsync(string? termoBusca, int pagina, int tamanhoPagina, bool ordemDesc);
        Task AddProduto(Produto produto);
        void UpdateProduto(Produto produto);
        void DeleteProduto(Produto produto);
        Task<bool> SaveChangesAsync();
    }
}