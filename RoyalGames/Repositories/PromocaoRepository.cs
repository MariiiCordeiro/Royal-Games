using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;
using System.Runtime.InteropServices;

namespace RoyalGames.Repositories
{
    public class PromocaoRepository : IPromocaoRepository
    {
        private readonly RoyalGamesContext _context;

        public PromocaoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Promocao> Listar()
        {
            var promocoes = _context.Promocao.ToList();
            return promocoes;
        }

        public Promocao ObterPorId(int id)
        {
            return _context.Promocao.Find(id);
        }

        public bool NomeExiste(string nome, int? promocaoIdAtual = null)
        {
            var promocaoBanco = _context.Promocao.AsQueryable();

            if (promocaoIdAtual.HasValue)
                promocaoBanco = promocaoBanco.Where(p => p.PromocaoId == promocaoIdAtual.Value);

            return promocaoBanco.Any(p => p.Nome == nome);
        }

        public void Adicionar(Promocao promocao)
        {
            _context.Add(promocao);
            _context.SaveChanges();
        }

        public void Atualizar(Promocao promocao)
        {
            Promocao promocaoBanco = _context.Promocao.FirstOrDefault(p => p.PromocaoId == promocao.PromocaoId);
            if (promocaoBanco == null)
                return;

            promocaoBanco.Nome = promocao.Nome;
            promocaoBanco.DataExpiracao = promocao.DataExpiracao;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Promocao promocao = _context.Promocao.Find(id);
            if (promocao == null)
                return;

            _context.Remove(promocao);
            _context.SaveChanges();
        }
    }
}
