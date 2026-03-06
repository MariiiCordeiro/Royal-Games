namespace RoyalGames.DTOs.PromocaoDto
{
    public class AtualizarPromocaoDto
    {
        public string Nome { get; set; } = null!;

        public DateTime DataExpiracao { get; set; }

        public string Descricao { get; set; } = null!;

        public bool? StatusPromocao { get; set; }
    }
}
