using MarketPlace.Interface.Service;
using MarketPlace.Models;
using MarketPlace.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProdutos()
        {
            try
            {
                var result = await _produtoService.ObterTodosProtudosAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de produto. Exceçao {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto([FromRoute] Guid id)
        {
            try
            {
                Produto produto = await _produtoService.ObterProdutorPorIdAsync(id);
                if (produto != null)
                {
                    return Ok(produto);
                }
                else
                {
                    return NotFound("Erro, produto nao existe");
                }

            }
            catch (Exception e)
            {

                return BadRequest($"Erro na Procura de produto. Exceçao {e.Message}");
            }
        }

        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetProdutoPesquisa([FromQuery] string valor)
        {
            try
            {
                var listaDeProdutos = await _produtoService.PesquisaPorProdutoAsync(valor);

                if (listaDeProdutos == null || !listaDeProdutos.Any())
                {
                    return NotFound("Nenhum produto foi encontrado com o termo informado.");
                }

                return Ok(listaDeProdutos);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, pesquisa de Produto. Exceçao: {e.Message}");
            }
        }

        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetProdutoPaginacao([FromQuery] string? valor, int pagina, int tamanhoPagina, bool ordemDesc)
        {
            try
            {
                var resultadoPaginacao = await _produtoService.ObterPessoasPaginadoAsync(valor, pagina, tamanhoPagina, ordemDesc);
                return Ok(resultadoPaginacao);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, Paginacao de Pessoa. Exceçao: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] CriarProdutoDto CriarProdutoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var produtoCriado = await _produtoService.CriarNovoProdutoAsync(CriarProdutoDto);
                return CreatedAtAction(nameof(GetProduto), new { id = produtoCriado.Id }, produtoCriado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex.Message} --- DETALHES (Inner Exception): {ex.InnerException?.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(Guid id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest("O ID da rota não corresponde ao ID do produto.");
            }
            try
            {
                await _produtoService.AtualizarProdutoAsync(produto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Erro na alteraçao de Produto. Exceçao {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto([FromRoute] Guid id)
        {
            try
            {
                await _produtoService.DeletarProdutoAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro ao deletar produto. Exceçao {e.Message}");
            }
        }
    }
}