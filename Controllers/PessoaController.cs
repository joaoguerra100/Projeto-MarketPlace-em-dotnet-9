using MarketPlace.Data;
using MarketPlace.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PessoaController : Controller
    {
        private readonly MarketPlaceDbContext _context;
        
        public PessoaController(MarketPlaceDbContext marketPlaceDbContext)
        {
            _context = marketPlaceDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostPessoa([FromBody] Pessoa pessoa)
        {
            try
            {
                await _context.Pessoa.AddAsync(pessoa);
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