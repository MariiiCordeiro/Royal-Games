using RoyalGames.Domains;
using RoyalGames.DTOs.AutenticacaoDto;
using RoyalGames.DTOs.PromocaoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using VHBurger.Aplications.Rules;

namespace RoyalGames.Aplications.Services
{
    public class PromocaoService
    {
        private readonly IPromocaoRepository _repository;

        public PromocaoService(IPromocaoRepository repository)
        {
            _repository = repository;
        }

        public List<LerPromocaoDto> Listar()
        {
            List<Promocao> promocoes = _repository.Listar();
            List<LerPromocaoDto> promocaoDto = promocoes.Select(p => new LerPromocaoDto
            {
                PromocaoId = p.PromocaoId,
                Nome = p.Nome,
            }).ToList();

            return promocaoDto;
        }

        public Promocao ObterPorId(int id)
        {
            Promocao promocao = _repository.ObterPorId(id);
            if (promocao == null)
            {
                throw new DomainException("Promocão não encontrada!");
            }

            LerPromocaoDto promocaoDto = new LerPromocaoDto
            {
                PromocaoId = promocao.PromocaoId,
                Nome = promocao.Nome
            };

            return promocao;
        }

        private static void ValidarCadastro(CriarPromocaoDto promocao)
        {

            if (string.IsNullOrWhiteSpace(promocao.Nome))
            {
                throw new DomainException("Nome é obrigatório!");
            }

            if (promocao.DataExpiracao < DateTime.Now.Date)
            {
                throw new DomainException("Data precisa ser maior que a data atual!");
            }

            if (string.IsNullOrWhiteSpace(promocao.Descricao))
            {
                throw new DomainException("Descrição é obrigatória!");
            }
        }

        public void Adicionar(CriarPromocaoDto promocaoDto)
        {
            ValidarCadastro(promocaoDto);

            if (_repository.NomeExiste(promocaoDto.Nome) == true)
            {
                throw new DomainException("Promoção já cadastrada");
            }

            Promocao promocao = new Promocao
            {
                Nome = promocaoDto.Nome,
                DataExpiracao = promocaoDto.DataExpiracao,
                Descricao = promocaoDto.Descricao,
            };

            _repository.Adicionar(promocao);
        }
        private void ValidarNome(AtualizarPromocaoDto promocaoDto)
        {
            if (string.IsNullOrWhiteSpace(promocaoDto.Nome))
            {
                throw new DomainException("Nome é obrigatório!");
            }
        }

        public void Atualizar(int id, AtualizarPromocaoDto promocaoDto)
        {
            HorarioAlteracaoProduto.ValidarHorario();

            Promocao promocaoBanco = _repository.ObterPorId(id);

            if (promocaoBanco == null)
            {
                throw new DomainException("Promoção não encontrada!");
            }

            ValidarNome(promocaoDto);

            Promocao promocao = _repository.ObterPorId(id);

            if (_repository.NomeExiste(promocaoDto.Nome))
                throw new DomainException("Promoção já cadastrada!");

            promocao.Nome = promocaoDto.Nome;

            if (promocaoDto.DataExpiracao <= promocao.DataExpiracao)
                return;

            promocao.DataExpiracao = promocaoDto.DataExpiracao;
            promocao.Descricao = promocaoDto.Descricao;

            _repository.Atualizar(promocao);
        }

        public void Remover(int id)
        {
            Promocao promocao = _repository.ObterPorId(id);
            if (promocao == null)
            {
                throw new DomainException("Promoção não encontrada!");
            }

            _repository.Remover(id);
        }
    }
}
