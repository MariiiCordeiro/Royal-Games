namespace RoyalGames.DTOs.ProdutoDto
{
    public class LerFiltroProdutoDto
    {
        public int? ProdutoID { get; set; } = null;

        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public bool? StatusProduto { get; set; }

        // Generos
        public List<int> GeneroIds { get; set; } = new();
        public List<string> Generos { get; set; } = new();

        // Plataforma
        public List<int> PlataformaIds { get; set; } = new();
        public List<string> Plataforma { get; set; } = new();
    }
}
