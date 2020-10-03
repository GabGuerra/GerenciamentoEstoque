using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoIdentidadeCore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Services.Movimentacao
{
    public interface ImovimentacaoService
    {
        public ResultadoVD MovimentarProduto(Imovimentacao Movimentacao);        
    }
}
