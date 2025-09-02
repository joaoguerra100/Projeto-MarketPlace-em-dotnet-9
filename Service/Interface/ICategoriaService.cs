using MarketPlace.Models;

namespace MarketPlace.Interface.Service
{
    public interface ICategoriaService
    {
        Task<Categoria?> ObterCategoriaPorIdAsync(Guid id);
        Task<IEnumerable<Categoria>> ObterTodasCategoriasAsync();
        Task<Categoria?> PesquisarPorNomeAsync(string nome);
        Task<PaginacaoResponse<Categoria>> ObterCategoriaPaginadoAsync(string? termoBusca, int pagina, int tamanhoPagina, bool ordemDesc);
        Task<Categoria> CriarNovaCategoria(Categoria categoria);
        Task AtualizarAsync(Categoria categoria);
        Task DeletarAsync(Guid id);
    }
}
