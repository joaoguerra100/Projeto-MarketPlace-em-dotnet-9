using MarketPlace.Data;
using MarketPlace.Interface.Repositorie;
using MarketPlace.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Repositorie
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MarketPlaceDbContext _context;

        public ProdutoRepository(MarketPlaceDbContext context)
        {
            _context = context;
        }

        public async Task<Produto?> GetProdutoById(Guid id)
        {
            return await _context.Produto.FindAsync(id);
        }

        public async Task<IEnumerable<Produto>> GetAllProduto()
        {
            return await _context.Produto.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> PesquisaPorProdutoAsync(string valor)
        {
            var termoBusca = valor.ToUpper();

            var query = from o in _context.Produto
                        where o.Nome.ToUpper().Contains(termoBusca)
                        || o.Status.ToUpper().Contains(termoBusca)
                        select o;

            var lista = await query.ToListAsync();

            return lista;
        }

        public async Task<PaginacaoResponse<Produto>> ObterPaginadoAsync(string? termoBusca, int pagina, int tamanhoPagina, bool ordemDesc)
        {
            //Passo 1. Começamos com IQueryable para construir a consulta dinamicamente
            IQueryable<Produto> query = _context.Produto.AsQueryable();

            //Passo 2: Aplicar filtro se possuir 
            if (!string.IsNullOrEmpty(termoBusca))
            {
                var termo = termoBusca.ToUpper();
                query = query.Where(o => o.Nome.ToUpper().Contains(termo)
                        || o.Status.ToUpper().Contains(termo));
            }

            //Passo 3: Obter o total de linhas antes de paginar
            var totalLinhas = await query.CountAsync();

            //Passo 4 aplicar ordenação
            if (ordemDesc)
            {
                query = query.OrderByDescending(p => p.Nome);
            }
            else
            {
                query = query.OrderBy(p => p.Nome);
            }

            //Passo 5: Aplicar a paginaçao de (skip/take)
            var dadosDaPagina = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            //Passo 6: Montar e retornar o objeto de resposta
            return new PaginacaoResponse<Produto>(dadosDaPagina, totalLinhas, pagina, tamanhoPagina);
        }

        public async Task AddProduto(Produto produto)
        {
            await _context.Produto.AddAsync(produto);
        }

        public void UpdateProduto(Produto produto)
        {
            _context.Produto.Update(produto);
        }

        public void DeleteProduto(Produto produto)
        {
            _context.Produto.Remove(produto);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}