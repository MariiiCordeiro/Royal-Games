using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface ILogAlteracaoProdutoRepository
    {
        List<Log_AlteracaoProduto> Listar();
        List<Log_AlteracaoProduto> ListarPorProduto(int produtoId);

    }
}
