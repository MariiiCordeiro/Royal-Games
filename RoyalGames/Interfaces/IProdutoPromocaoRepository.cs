using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IProdutoPromocaoRepository
    {
        List<ProdutoPromocao> Listar();
        ProdutoPromocao ListarProdutoPromocaoPorIds(int? promocaoId = null, int? produtoId = null);
        ProdutoPromocao LerParaAtualizarProdutoPromocaoDto(ProdutoPromocao produtoPromocao, int? promocaoId = null, int? produtoId = null);
        bool ProdutoPromocaoExiste(int promocaoId, int produtoId);
        void Adicionar(ProdutoPromocao produtoPromocao);
        void Atualizar(int promocaoId, int produtoId, decimal valor);
        void Remover(int promocaoId, int produtoId);
    }
}
