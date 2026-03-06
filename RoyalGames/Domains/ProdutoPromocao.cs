using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class ProdutoPromocao
{
    public int PromocaoId { get; set; }

    public int ProdutoId { get; set; }

    public decimal? Valor { get; set; }

    public virtual Produto Produto { get; set; } = null!;

    public virtual Promocao Promocao { get; set; } = null!;
}
