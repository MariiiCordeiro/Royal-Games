using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Aplications.Services;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogProdutoController : ControllerBase
    {
        private readonly LogAlteracaoProdutoService _service;

        public LogProdutoController(LogAlteracaoProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("produto/{id}")]
        public ActionResult ListarPorProduto(int id)
        {
            return Ok(_service.ListarPorProduto(id));
        }
    }
}
