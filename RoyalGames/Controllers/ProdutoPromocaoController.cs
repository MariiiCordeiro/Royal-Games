using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Aplications.Services;
using RoyalGames.Domains;
using RoyalGames.DTOs.ProdutoPromocao;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoPromocaoController : ControllerBase
    {
        private readonly ProdutoPromocaoService _service;

        public ProdutoPromocaoController(ProdutoPromocaoService produtoPromocaoService)
        {
            _service = produtoPromocaoService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var produtoPromocao = _service.Listar();
                return Ok(produtoPromocao);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("promocaoId/{promocaoId}/produtoId/{produtoId}")]
        public IActionResult ListarProdutoPromocaoPorIds(int promocaoId, int produtoId)
        {
            try
            {
                var produtoPromocao = _service.ListarProdutoPromocaoPorIds(promocaoId, produtoId);
                return Ok(produtoPromocao);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Adicionar([FromBody] CriarProdutoPromocaoDto criarProdutoPromocaoDto)
        {
            try
            {
                _service.Adicionar(criarProdutoPromocaoDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult Atualizar(int promocaoId, int produtoId, decimal valor)
        {
            try
            {
                AtualizarProdutoPromocaoDto atualizarProdutoPromocaoDto = _service.LerParaAtualizarProdutoPromocaoDto(promocaoId, produtoId);
                _service.Atualizar(promocaoId, produtoId, valor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("promocaoId/{promId}/produtuoId/{prodId}")]
        [Authorize]
        public IActionResult Remover(int promId, int prodId)
        {
            try
            {
                _service.Remover(promId, prodId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

