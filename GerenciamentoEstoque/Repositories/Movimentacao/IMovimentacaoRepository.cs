using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoIdentidadeCore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Repositories.Movimentacao
{
    public interface IMovimentacaoRepository
    {
        public ResultadoVD MovimentarProdutos(MovimentacaoVD Movimentacao);
        public List<MovimentacaoVD> ListarMovimentacoesCliente(int codCliente);
        public MovimentacaoVD GerarNotaFiscal(int codCliente);
    }
}
