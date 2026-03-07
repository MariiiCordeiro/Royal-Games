using Microsoft.AspNetCore.Http.HttpResults;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.DTOs.ProdutoPromocao;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class ProdutoPromocaoRepository : IProdutoPromocaoRepository
    {
        private readonly RoyalGamesContext _context;
        public ProdutoPromocaoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<ProdutoPromocao> Listar()
        {
            List<ProdutoPromocao> produtoPromocao = _context.ProdutoPromocao.ToList();
            return produtoPromocao;
        }

        public ProdutoPromocao ListarProdutoPromocaoPorIds(int? promocaoId = null, int? produtoId = null)
        {
            ProdutoPromocao produtoPromocao = _context.ProdutoPromocao.FirstOrDefault(pp => pp.PromocaoId == promocaoId && pp.ProdutoId == produtoId);
            return produtoPromocao;
        }

        public ProdutoPromocao LerParaAtualizarProdutoPromocaoDto(ProdutoPromocao produtoPromocao, int? promocaoId = null, int? produtoId = null)
        {
            ProdutoPromocao produtoPromocaoBanco = _context.ProdutoPromocao.FirstOrDefault(pp => pp.PromocaoId == promocaoId && pp.ProdutoId == produtoId);
            return produtoPromocaoBanco;
        }

        public bool ProdutoPromocaoExiste(int produtoId, int promocaoId)
        {
            if (produtoId == null || promocaoId == null)
                throw new Exception("Promoção / Produto não localizado");

            return _context.ProdutoPromocao.Any(pp => pp.ProdutoId == produtoId && pp.PromocaoId == promocaoId);
        }

        public void Adicionar(ProdutoPromocao produtoPromocao)
        {
            var promocao = _context.Promocao.FirstOrDefault(p => p.PromocaoId == produtoPromocao.PromocaoId);
            if (promocao == null)
                return;

            var produto = _context.Produto.FirstOrDefault(p => p.ProdutoID == produtoPromocao.ProdutoId);
            if (produto == null)
                return;

            ProdutoPromocao produtoAdiconado = new ProdutoPromocao
            {
                PromocaoId = produtoPromocao.PromocaoId,
                ProdutoId = produtoPromocao.ProdutoId,
                Valor = produtoPromocao.Valor,
                Produto = produto,
                Promocao = promocao
            };

            _context.ProdutoPromocao.Add(produtoAdiconado);
            _context.SaveChanges();
        }

        public void Atualizar(int promocaoId, int produtoId, decimal valor)
        {
            ProdutoPromocao produtoPromocaoBanco = _context.ProdutoPromocao.FirstOrDefault(pp => pp.ProdutoId == produtoId && pp.PromocaoId == promocaoId);
            if (produtoPromocaoBanco == null)
                return;

            produtoPromocaoBanco.PromocaoId = promocaoId;
            produtoPromocaoBanco.ProdutoId = produtoId;
            produtoPromocaoBanco.Valor = valor;

            _context.Update(produtoPromocaoBanco);
            _context.SaveChanges(true);
        }

        public void Remover(int promocaoId, int produtoId)
        {
            var produtoPromocaoBanco = _context.ProdutoPromocao.FirstOrDefault(pp => pp.PromocaoId == promocaoId && pp.ProdutoId == produtoId);

            if (produtoPromocaoBanco == null)
                return;

            _context.ProdutoPromocao.Remove(produtoPromocaoBanco);
            _context.SaveChanges();
        }
    }
}
