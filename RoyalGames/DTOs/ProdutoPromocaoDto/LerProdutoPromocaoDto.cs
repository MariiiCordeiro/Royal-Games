using RoyalGames.Domains;

namespace RoyalGames.DTOs.ProdutoPromocao
{
    public class LerProdutoPromocaoDto
    {
        public int PromocaoId { get; set; }

        public int ProdutoId { get; set; }

        public decimal? Valor { get; set; }
    }
}
