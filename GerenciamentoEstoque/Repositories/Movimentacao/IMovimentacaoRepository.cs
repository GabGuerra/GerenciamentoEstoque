using GerenciamentoEstoque.Models.Documento;
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
        public ResultadoVD MovimentarProdutos(DocumentoVD Movimentacao);
        public List<DocumentoVD> ListarMovimentacoesCliente(int codCliente);
        public DocumentoVD GerarNotaFiscal(int codCliente);
    }
}
