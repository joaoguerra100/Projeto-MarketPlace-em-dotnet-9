using MarketPlace.Data;
using MarketPlace.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoriaController : Controller
    {
        private readonly MarketPlaceDbContext _context;

        public CategoriaController(MarketPlaceDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostPessoa([FromBody] Categoria categoria)
        {
            try
            {
                await _context.Categoria.AddAsync(categoria);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Sucesso, pessoa incluida");
                }
                else
                {
                    return BadRequest("Erro, pessoa nao incluida");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na inclusao de pessoa. Exceçao {e.Message}");
            }
        }
    }
}