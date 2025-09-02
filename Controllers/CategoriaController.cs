using MarketPlace.Interface.Service;
using MarketPlace.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService produtoService)
        {
            _categoriaService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategorias()
        {
            try
            {
                var result = await _categoriaService.ObterTodasCategoriasAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de categoria. Exceçao {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria([FromRoute] Guid id)
        {
            try
            {
                Categoria categoria = await _categoriaService.ObterCategoriaPorIdAsync(id);
                if (categoria != null)
                {
                    return Ok(categoria);
                }
                else
                {
                    return NotFound("Erro, categoria nao existe");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na Procura de categoria. Exceçao {e.Message}");
            }
        }

        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetCategoriaPesquisa([FromQuery] string nome)
        {
            try
            {
                var listaDeCategorias = await _categoriaService.PesquisarPorNomeAsync(nome);

                if (listaDeCategorias == null)
                {
                    return NotFound("Nenhuma categoria foi encontrado com o termo informado.");
                }

                return Ok(listaDeCategorias);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, pesquisa de Categoria. Exceçao: {e.Message}");
            }
        }

        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetCategoriaPaginacao([FromQuery] string? valor, int pagina, int tamanhoPagina, bool ordemDesc)
        {
            try
            {
                var resultadoPaginacao = await _categoriaService.ObterCategoriaPaginadoAsync(valor, pagina, tamanhoPagina, ordemDesc);
                return Ok(resultadoPaginacao);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, Paginacao de categoria. Exceçao: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarCategoria([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var categoriaCriado = await _categoriaService.CriarNovaCategoria(categoria);
                return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex.Message} (Inner Exception): {ex.InnerException?.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCategoria(Guid id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest("O ID da rota não corresponde ao ID do categoria.");
            }
            try
            {
                await _categoriaService.AtualizarAsync(categoria);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro na alteraçao de categoria. Exceçao {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria([FromRoute] Guid id)
        {
            try
            {
                await _categoriaService.DeletarAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro ao deletar categoria. Exceçao {e.Message}");
            }
        }
    }
}