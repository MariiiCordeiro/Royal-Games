using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IClassificacaoRepository
    {
        List<ClassificacaoIndicativa> Listar();

        ClassificacaoIndicativa ObterPorId(int id);
    }
}
