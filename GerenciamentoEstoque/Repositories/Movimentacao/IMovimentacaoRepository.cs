using GerenciamentoEstoque.Models.Movimentacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Repositories.Movimentacao
{
    public interface IMovimentacaoRepository
    {
        public MovimentacaoVD MovimentarProduto(Imovimentacao Movimentacao);
    }
}
