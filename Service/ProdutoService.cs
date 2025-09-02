using MarketPlace.Interface.Repositorie;
using MarketPlace.Interface.Service;
using MarketPlace.Models;
using MarketPlace.Models.Dto;

namespace MarketPlace.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Produto?> ObterProdutorPorIdAsync(Guid id)
        {
            //Talvez colocar logica de cache,logs
            return await _produtoRepository.GetProdutoById(id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosProtudosAsync()
        {
            return await _produtoRepository.GetAllProduto();
        }

        public async Task<IEnumerable<Produto>> PesquisaPorProdutoAsync(string valor)
        {
            //Passo 1: verifica o input para nao deixar espaço em branco
            if (string.IsNullOrEmpty(valor))
            {
                return Enumerable.Empty<Produto>();
            }   

            // ORQUESTRAÇÃO: Chamar o repositório para fazer o trabalho pesado no banco de dados.
            var produtosEncontrados = await _produtoRepository.PesquisaPorProdutoAsync(valor);

            //Passo 2: Talvez registrar os logs de pesquisa feito
            // Log.Information($"Pesquisa realizada pelo termo: {termo}");

            //Passo 3: Retornar a lista de produtos encontrados
            return produtosEncontrados;
        }

        public async Task<PaginacaoResponse<Produto>> ObterPessoasPaginadoAsync(string? termoBusca, int pagina, int tamanhoPagina, bool ordemDesc)
        {
            //Passo 1: Regra de negócio, limita o tamanho de paginas para no maximo de 100 registros
            if (tamanhoPagina > 100)
            {
                tamanhoPagina = 100;
            }

            //Passo 2: Regra de negócio, apagina deve ser no minimo 1
            if (pagina < 1)
            {
                pagina = 1;
            }

            return await _produtoRepository.ObterPaginadoAsync(termoBusca, pagina, tamanhoPagina, ordemDesc);
        }

        public async Task<Produto> CriarNovoProdutoAsync(CriarProdutoDto CriarProdutoDto)
        {
            //Passo 1: Checa se o preço do produto e maior ou igual a zero, se ele e valido
            if (CriarProdutoDto.Preco <= 0)
            {
                throw new ArgumentException("O preço do Produto nao pode ser zero ou negativo.");
            }

            //Passo 2: Mapeamento do DTO para a Entidade Produto
            var novoProduto = new Produto
            {
                Nome = CriarProdutoDto.Nome,
                Descricao = CriarProdutoDto.Descricao,
                Preco = CriarProdutoDto.Preco,
                QtdEstoque = CriarProdutoDto.QtdEstoque,
                VendedorId = CriarProdutoDto.VendedorId,
                CategoriaId = CriarProdutoDto.CategoriaId,
                //Passo 3: Aplicação da Lógica de Negócio (Regras do Servidor)
                Status = "Ativo",
                DataDeCriacao = DateTime.UtcNow,
                DataDeAtualizacao = DateTime.UtcNow
            };

            //Passo 3: Persistência
            await _produtoRepository.AddProduto(novoProduto);
            await _produtoRepository.SaveChangesAsync();
            
            return novoProduto;
        }

        public async Task AtualizarProdutoAsync(Produto produto)
        {
            //Passo 1: Verifica se o produto a ser atualizado existe
            var produtoExistente = await _produtoRepository.GetProdutoById(produto.Id);
            if (produtoExistente == null)
            {
                throw new KeyNotFoundException("Produto nao encontrado para Atualização.");
            }

            //Passo 2 Aplicar as novas propriedades
            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.QtdEstoque = produto.QtdEstoque;
            produtoExistente.Status = produto.Status;
            produtoExistente.DataDeAtualizacao = DateTime.UtcNow;

            _produtoRepository.UpdateProduto(produtoExistente);
            await _produtoRepository.SaveChangesAsync();
        }

        public async Task DeletarProdutoAsync(Guid id)
        {
            var produtoExistente = await _produtoRepository.GetProdutoById(id);
            if (produtoExistente == null)
            {
                throw new KeyNotFoundException("Produto nao encontrado para Deletar.");
            }

            _produtoRepository.DeleteProduto(produtoExistente);
            await _produtoRepository.SaveChangesAsync();
        }
    }
}