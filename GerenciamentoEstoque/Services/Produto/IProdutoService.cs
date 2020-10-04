using GerenciamentoEstoque.Models.Produto;
using GerenciamentoIdentidadeCore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Services.Produto
{
    public interface IProdutoService
    {
        public ResultadoVD InserirProduto(ProdutoVD Produto);
        public ResultadoVD EditarProduto(ProdutoVD Produto);
        public ResultadoVD RemoverProduto(int codProduto);
        public List<ProdutoVD> CarregarListaProduto();
        public void AtualizarPrecoCustoMedioProduto(int codProduto, double custoUnitarioMovimentacao, int qtdMovimentada);

    }
}
