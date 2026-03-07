using RoyalGames.Domains;
using RoyalGames.DTOs.ProdutoDto;

namespace RoyalGames.Aplications.Conversions
{
    public class FiltrarProdutoParaDto
    {
        public static LerFiltroProdutoDto ConverterParaDto(Produto produto)
        {
            return new LerFiltroProdutoDto
            {
                ProdutoID = produto.ProdutoID,
                Nome = produto.Nome,
                Preco = produto.Preco,
                StatusProduto = produto.StatusProduto,

                GeneroIds = produto.Genero.Select(categoria => categoria.GeneroID).ToList(),

                Generos = produto.Genero.Select(categoria => categoria.Nome).ToList(),

                PlataformaIds = produto.Genero.Select(categoria => categoria.GeneroID).ToList(),

                Plataforma = produto.Genero.Select(categoria => categoria.Nome).ToList(),
            };
        }
    }
}
