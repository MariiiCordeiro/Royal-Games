using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Aplications.Services;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.DTOs.PromocaoDto;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocaoController : ControllerBase
    {
        private readonly RoyalGamesContext _context;
        private readonly PromocaoService _service;

        public PromocaoController(RoyalGamesContext context, PromocaoService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            List<LerPromocaoDto> promocoes = _service.Listar().ToList();
            return Ok(promocoes);
        }

        [HttpGet("{id}")]
        public IActionResult OberPorId(int id)
        {
            var promocao = _service.ObterPorId(id);

            if (promocao == null)
                return NotFound();

            return Ok(promocao);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Adicionar(CriarPromocaoDto criarPromocaoDto)
        {
            try
            {
                _service.Adicionar(criarPromocaoDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Atualizar(int id, AtualizarPromocaoDto promocaoDto)
        {
            try
            {
                _service.Atualizar(id, promocaoDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
