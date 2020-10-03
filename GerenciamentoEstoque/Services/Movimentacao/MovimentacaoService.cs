using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoEstoque.Repositories.Movimentacao;
using GerenciamentoIdentidadeCore2.Models;

namespace GerenciamentoEstoque.Services.Movimentacao
{
    public class MovimentacaoService : ImovimentacaoService
    {
        public IMovimentacaoRepository _movimentacaoRepository { get; set; }
        public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
        }
        public ResultadoVD MovimentarProduto(Imovimentacao Movimentacao)
        {
            ResultadoVD resultado = new ResultadoVD(true);
            try
            {
                resultado.ObjetoResultado = _movimentacaoRepository.MovimentarProduto(Movimentacao);
            }
            catch (Exception ex)
            {
                resultado.Mensagem = ex.Message;
                resultado.Sucesso = false;
            }

            return resultado;
        }
    }
}
