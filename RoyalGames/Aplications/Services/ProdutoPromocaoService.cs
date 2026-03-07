using Microsoft.Identity.Client;
using RoyalGames.Domains;
using RoyalGames.DTOs.ProdutoPromocao;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Repositories;

namespace RoyalGames.Aplications.Services
{
    public class ProdutoPromocaoService
    {
        private readonly IProdutoPromocaoRepository _repository;
        public ProdutoPromocaoService(IProdutoPromocaoRepository repository)
        {
            _repository = repository;
        }

        public LerProdutoPromocaoDto LerDto(ProdutoPromocao produtosPromoBanco)
        {
            return new LerProdutoPromocaoDto
            {
                ProdutoId = produtosPromoBanco.ProdutoId,
                PromocaoId = produtosPromoBanco.PromocaoId,
                Valor = produtosPromoBanco.Valor
            };
        }

        public AtualizarProdutoPromocaoDto LerParaAtualizarDto(ProdutoPromocao produtosPromoBanco)
        {
            AtualizarProdutoPromocaoDto lerProdutoPromocao = new AtualizarProdutoPromocaoDto
            {
                Valor = produtosPromoBanco.Valor
            };
            return lerProdutoPromocao;
        }

        public List<LerProdutoPromocaoDto> Listar()
        {
            List<ProdutoPromocao> produtosPromo = _repository.Listar();
            List<LerProdutoPromocaoDto> lerProdutosPromo = produtosPromo.Select(produtosPromoBanco => LerDto(produtosPromoBanco)).ToList();
            return lerProdutosPromo;
        }

        public LerProdutoPromocaoDto ListarProdutoPromocaoPorIds(int promocaoId, int produtoId)
        {
            var produtoPromo = _repository.ListarProdutoPromocaoPorIds(promocaoId, produtoId);

            if (produtoPromo == null)
                throw new DomainException("Produto / Promoção não encontrado(s)!");

            return LerDto(produtoPromo);
        }

        public AtualizarProdutoPromocaoDto LerParaAtualizarProdutoPromocaoDto(int promocaoId, int produtoId)
        {
            var produtoPromo = _repository.ListarProdutoPromocaoPorIds(promocaoId, produtoId);

            if (produtoPromo == null)
                throw new DomainException("Produto / Promoção não encontrado(s)!");

            return LerParaAtualizarDto(produtoPromo);
        }

        public void Adicionar(CriarProdutoPromocaoDto produtoPromocao)
        {
            if (_repository.ProdutoPromocaoExiste(produtoPromocao.ProdutoId, produtoPromocao.PromocaoId) == true)
                throw new DomainException("Produto ou promoção já cadastrado!");

            var NovoProdutoPromoBanco = new ProdutoPromocao
            {
                PromocaoId = produtoPromocao.PromocaoId,
                ProdutoId = produtoPromocao.ProdutoId,
                Valor = produtoPromocao.Valor,
            };

            _repository.Adicionar(NovoProdutoPromoBanco);
        }

        public void Atualizar(int promocaoId, int produtoId, decimal valor)
        {
            var produtoPromoBanco = _repository.ListarProdutoPromocaoPorIds(promocaoId, produtoId);
            if (produtoPromoBanco == null)
                throw new DomainException("Produto / Promoção não encontrado(s)!");

            var NovoProdutoPromoBanco = new ProdutoPromocao
            {
                PromocaoId = promocaoId,
                ProdutoId = produtoId,
                Valor = valor,
            };

            _repository.Atualizar(promocaoId, produtoId, valor);
        }

        public void Remover(int promocaoId, int produtoId)
        {
            var produtoPromoBanco = _repository.ListarProdutoPromocaoPorIds(promocaoId, produtoId);
            if (produtoPromoBanco == null)
                throw new DomainException("Produto / Promoção não encontrado(s)!");

            _repository.Remover(promocaoId, produtoId);
        }
    }
}
