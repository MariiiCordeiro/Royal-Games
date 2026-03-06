using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class Promocao
{
    public int PromocaoId { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime DataExpiracao { get; set; }

    public string Descricao { get; set; } = null!;

    public bool? StatusPromocao { get; set; }

    public virtual ICollection<ProdutoPromocao> ProdutoPromocao { get; set; } = new List<ProdutoPromocao>();
}
