using MarketPlace.Models;
using MarketPlace.Models.Dto;

namespace MarketPlace.Interface.Service
{
    public interface IProdutoService
    {
        Task<Produto?> ObterProdutorPorIdAsync(Guid id);
        Task<IEnumerable<Produto>> ObterTodosProtudosAsync();
        Task<IEnumerable<Produto>> PesquisaPorProdutoAsync(string valor);
        Task<PaginacaoResponse<Produto>> ObterPessoasPaginadoAsync(string? termoBusca, int pagina, int tamanhoPagina, bool ordemDesc);
        Task<Produto> CriarNovoProdutoAsync(CriarProdutoDto CriarProdutoDto);
        Task AtualizarProdutoAsync(Produto produto);
        Task DeletarProdutoAsync(Guid id);
    }
}