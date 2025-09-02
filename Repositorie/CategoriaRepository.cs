using MarketPlace.Data;
using MarketPlace.Interface.Repositorie;
using MarketPlace.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Repositorie
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly MarketPlaceDbContext _context;

        public CategoriaRepository(MarketPlaceDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria?> GetByIdAsync(Guid id)
        {
            var result = await _context.Categoria
                        .Include(c => c.CategoriaPai)    // <-- INCLUI O PAI
                        .Include(c => c.SubCategorias)   // <-- INCLUI OS FILHOS
                        .FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            var result = await _context.Categoria.ToListAsync();
            return result;
        }

        public async Task<Categoria?> PesquisaPorNome(string nome)
        {
            var result = await _context.Categoria.FirstOrDefaultAsync(c => c.Nome == nome);
            return result;
        }

        public async Task<PaginacaoResponse<Categoria>> ObterPaginadoAsync(string? termoBusca, int pagina, int tamanhoPagina, bool ordemDesc)
        {
            IQueryable<Categoria> query = _context.Categoria.AsQueryable();

            if (!string.IsNullOrEmpty(termoBusca))
            {
                var termo = termoBusca.ToUpper();
                query = query.Where(o => o.Nome.ToUpper().Contains(termo));
            }

            var totalLinhas = await query.CountAsync();

            if (ordemDesc)
            {
                query = query.OrderByDescending(p => p.Nome);
            }
            else
            {
                query = query.OrderBy(p => p.Nome);
            }

            var dadosDaPagina = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return new PaginacaoResponse<Categoria>(dadosDaPagina, totalLinhas, pagina, tamanhoPagina);
        }

        public async Task AddAscyn(Categoria categoria)
        {
            await _context.Categoria.AddAsync(categoria);
        }

        public void Update(Categoria categoria)
        {
            _context.Categoria.Update(categoria);
        }

        public void Delete(Categoria categoria)
        {
            _context.Categoria.Remove(categoria);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}