using GerenciamentoEstoque.Models.Fornecedor;
using GerenciamentoEstoque.Models.UnidadeMedida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Produto
{
    public class ProdutoVD 
    {
        public int? CodProduto { get; set; }
        public string NomeProduto { get; set; }
        public double? PrecoCustoMedio { get; set; }        
        public UnidadeMedidaVD UnidadeMedida { get; set; }
        public FornecedorVD Fornecedor { get; set; }

        public ProdutoVD()
        {
            UnidadeMedida = new UnidadeMedidaVD();
            Fornecedor = new FornecedorVD();
        }
        public ProdutoVD(string NomeProduto)
        {
        }        
    }
}
