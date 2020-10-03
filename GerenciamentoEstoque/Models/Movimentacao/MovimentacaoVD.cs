using GerenciamentoEstoque.Models.Deposito;
using GerenciamentoEstoque.Models.Documento;
using GerenciamentoEstoque.Models.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Movimentacao
{
    public class MovimentacaoVD: Imovimentacao
    {
        public ProdutoVD Produto { get; set; }
        public DepositoVD Deposito { get; set; }
        public int QtdMovimentacao { get; set; }
        public DateTime DatMovimentacao { get; set; }
        public TipoMovimentacaoVD TipoMovimentacao { get; set; }
        public DocumentoVD Documento { get; set; }
        public MovimentacaoVD()
        {
            this.Produto = new ProdutoVD();
            this.TipoMovimentacao = new TipoMovimentacaoVD();
            this.Deposito = new DepositoVD();
            this.Documento = new DocumentoVD();
        }
    }
}
