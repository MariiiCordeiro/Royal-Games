using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class ClassificacaoRepository : IClassificacaoRepository
    {
        private readonly RoyalGamesContext _context;

        public ClassificacaoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<ClassificacaoIndicativa> Listar()
        {
            return _context.ClassificacaoIndicativa.ToList();
        }

        public ClassificacaoIndicativa ObterPorId(int id)
        {
            return _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoID == id);
        }
    }
}
