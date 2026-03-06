using Microsoft.AspNetCore.Mvc;
using RoyalGames.Aplications.Services;
using static RoyalGames.Aplications.Services.ClassificacaoService;

namespace RoyalGames.Controllers
{
    [ApiController]
    [Route("api/classificacao")]
    public class ClassificacaoIndicativaController : ControllerBase
    {
        private readonly ClassificacaoService _service;

        public ClassificacaoIndicativaController(ClassificacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            return Ok(_service.ObterPorId(id));
        }
    }
}