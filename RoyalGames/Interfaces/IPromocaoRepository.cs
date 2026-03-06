using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IPromocaoRepository
    {
        List<Promocao> Listar();
        Promocao ObterPorId(int id);
        bool NomeExiste(string nome, int? PromocaoIdAtual = null);
        void Adicionar(Promocao promocao);
        void Atualizar(Promocao promocao);
        void Remover(int id);
    }
}
