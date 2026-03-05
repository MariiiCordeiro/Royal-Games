using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Aplications.Services
{
    public class ClassificacaoService
    {
        private readonly IClassificacaoRepository _repository;

        public ClassificacaoService(IClassificacaoRepository repository)
        {
            _repository = repository;
        }

        public List<ClassificacaoIndicativa> Listar()
        {
            return _repository.Listar();
        }

        public ClassificacaoIndicativa ObterPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
    }
}

