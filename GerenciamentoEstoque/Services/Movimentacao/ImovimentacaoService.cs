using GerenciamentoEstoque.Models.Documento;
using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoIdentidadeCore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Services.Movimentacao
{
    public interface IMovimentacaoService
    {        
        public ResultadoVD MovimentarProdutos(DocumentoVD Movimentacao);
        public ResultadoVD ListarMovimentacoesCliente(int Movimentacao);
        public ResultadoVD GerarNotaFiscal(int codMovimentacao);

    }
}
