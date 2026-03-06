namespace RoyalGames.DTOs.ProdutoDto
{ 
    // Pode inicializar vazio porque o filtro é opcional.
    public class FiltrarProdutoDto
    {
        public string? Nome { get; set; }
        public int? GeneroIds { get; set; }
        public int? PlataformaIds { get; set; }

        public bool? StatusProduto { get; set; }

        public decimal? PrecoMin { get; set; }

        public decimal? PrecoMax { get; set; }

    }
}
