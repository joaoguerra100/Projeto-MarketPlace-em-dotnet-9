using MarketPlace.Interface.Repositorie;
using MarketPlace.Interface.Service;
using MarketPlace.Models;

namespace MarketPlace.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria?> ObterCategoriaPorIdAsync(Guid id)
        {
            var result = await _categoriaRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<IEnumerable<Categoria>> ObterTodasCategoriasAsync()
        {
            var result = await _categoriaRepository.GetAllAsync();
            return result;
        }

        public async Task<Categoria?> PesquisarPorNomeAsync(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nao existe esta categoria com este nome");
            }

            var categoriasEncontradas = await _categoriaRepository.PesquisaPorNome(nome);

            return categoriasEncontradas;
        }

        public async Task<PaginacaoResponse<Categoria>> ObterCategoriaPaginadoAsync(string? termoBusca, int pagina, int tamanhoPagina, bool ordemDesc)
        {
            if (tamanhoPagina > 100)
            {
                tamanhoPagina = 100;
            }

            if (pagina < 1)
            {
                pagina = 1;
            }

            return await _categoriaRepository.ObterPaginadoAsync(termoBusca, pagina, tamanhoPagina, ordemDesc);
        }

        public async Task<Categoria> CriarNovaCategoria(Categoria categoria)
        {
            var categoriaExistente = await _categoriaRepository.PesquisaPorNome(categoria.Nome);
            if (categoriaExistente != null)
            {
                throw new ArgumentException("Já existe uma categoria com este nome.");
            }

            var novaCategoria = new Categoria
            {
                Nome = categoria.Nome,
                CategoriaPaiId = categoria.CategoriaPaiId
            };

            await _categoriaRepository.AddAscyn(novaCategoria);
            await _categoriaRepository.SaveChangesAsync();

            return novaCategoria;
        }

        public async Task AtualizarAsync(Categoria categoria)
        {
            var categoriaExistente = await _categoriaRepository.GetByIdAsync(categoria.Id);
            if (categoriaExistente == null)
            {
                throw new KeyNotFoundException("Categoria nao encontrado para Atualização.");
            }

            categoriaExistente.Nome = categoria.Nome;

            _categoriaRepository.Update(categoria);
            await _categoriaRepository.SaveChangesAsync();
        }

        public async Task DeletarAsync(Guid id)
        {
            var categoriaExistente = await _categoriaRepository.GetByIdAsync(id);
            if (categoriaExistente == null)
            {
                throw new KeyNotFoundException("Categoria nao encontrado para Deletar.");
            }

            _categoriaRepository.Delete(categoriaExistente);
            await _categoriaRepository.SaveChangesAsync();
        }
    }
}