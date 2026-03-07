using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.DTOs.ProdutoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RoyalGames.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly RoyalGamesContext _context;
        public ProdutoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Produto> Listar()
        {
            List<Produto> produtos = _context.Produto
                .Include(p => p.Genero)
                .Include(p => p.Usuario)
                .ToList();

            return produtos;
        }

        public List<Produto> Filtrar(FiltrarProdutoDto filtroDto)
        {
            // IQueryable permite montar a consulta dinamicamente.
            // A query só será executada quando chamarmos ToList().
            IQueryable<Produto> query = _context.Produto;

            // Verifica se o nome foi informado no filtro.
            // Se não for nulo ou vazio, filtra produtos que contenham esse nome.
            if (!string.IsNullOrWhiteSpace(filtroDto.Nome))
            {
                query = query.Where(p => p.Nome.Contains(filtroDto.Nome));
            }

            // Verifica se foi informado um ID de gênero.
            // HasValue indica que o campo nullable possui valor.
            if (filtroDto.GeneroIds.HasValue)
            {
                // Um produto pode ter vários gêneros.
                // O Any verifica se existe algum gênero do produto
                // com o ID informado no filtro.
                query = query.Where(p =>
                    p.Genero.Any(g => g.GeneroID == filtroDto.GeneroIds.Value));
            }

            // Verifica se foi informado um ID de plataforma.
            if (filtroDto.PlataformaIds.HasValue)
            {
                // Um produto pode estar disponível em várias plataformas.
                // O Any verifica se alguma plataforma do produto
                // corresponde ao ID informado no filtro.
                query = query.Where(p =>
                    p.Plataforma.Any(pl => pl.PlataformaID == filtroDto.PlataformaIds.Value));
            }

            // Filtra pelo status do produto (ativo ou inativo).
            if (filtroDto.StatusProduto.HasValue)
            {
                query = query.Where(p =>
                    p.StatusProduto == filtroDto.StatusProduto.Value);
            }

            // Filtra produtos com preço maior ou igual ao preço mínimo informado.
            if (filtroDto.PrecoMin.HasValue)
            {
                query = query.Where(p =>
                    p.Preco >= filtroDto.PrecoMin.Value);
            }

            // Filtra produtos com preço menor ou igual ao preço máximo informado.
            if (filtroDto.PrecoMax.HasValue)
            {
                query = query.Where(p =>
                    p.Preco <= filtroDto.PrecoMax.Value);
            }

            // Executa a consulta no banco de dados
            // e retorna a lista de produtos filtrados.
            return query.ToList();
        }

        public Produto ObterPorId(int id)
        {
            Produto? produto = _context.Produto
                .Include(p => p.Genero)
                .Include(p => p.Usuario)
                .FirstOrDefault(p => p.ProdutoID == id);

            if (produto == null)
                throw new DomainException("Produto não encontrado!");

            return produto;
        }

        public bool NomeExiste(string nome, int? produtoIdAtual = null)
        {
            var produtoBanco = _context.Produto.AsQueryable();

            if(produtoIdAtual.HasValue)
                produtoBanco = produtoBanco.Where(p => p.ProdutoID != produtoIdAtual.Value);

            return produtoBanco.Any(p => p.Nome == nome);
        }

        public byte[] ObterImagem(int id)
        {
            var imagemProduto = _context.Produto
                .Where(p => p.ProdutoID == id)
                .Select(p => p.Imagem)
                .FirstOrDefault();

            return imagemProduto;
        }

        public void Adicionar(Produto produto, List<int> generoIds)
        {
            // Busca e guarda em generos, todas as categorias cujo o id foi chamado.
            List<Genero> generos = _context.Genero
                .Where(g => generoIds.Contains(g.GeneroID))
                .ToList();

            produto.Genero = generos;

            _context.Add(produto);
            _context.SaveChanges();
        }

        public void Atualizar(Produto produto, List<int> generoIds)
        {
            Produto? produtoBanco = _context.Produto
                .Include(p => p.Genero)
                .FirstOrDefault(p => p.ProdutoID == produto.ProdutoID);

            if (produtoBanco == null)
                return;

            produtoBanco.Nome = produto.Nome;
            produtoBanco.Preco = produto.Preco;
            produtoBanco.Descricao = produto.Descricao; 

            if(produto.Imagem != null)
                produtoBanco.Imagem = produto.Imagem;

            if(produto.StatusProduto.HasValue)
                produtoBanco.StatusProduto = produto.StatusProduto.Value;

            // Busca e guarda em generos, todas as categorias cujo o id foi chamado.
            var generos = _context.Genero
                .Where(g => generoIds.Contains(g.GeneroID))
                .ToList();

            // Remover as com categorias atuais ligações atuais 
            produtoBanco.Genero.Clear();

            foreach(var genero in generos)
            {
                produtoBanco.Genero.Add(genero);
            }

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Produto produtoRemovido = _context.Produto.FirstOrDefault(p => p.ProdutoID == id);
            if (produtoRemovido == null)
                return;

            _context.Remove(produtoRemovido);
            _context.SaveChanges();


        }
    }
}
