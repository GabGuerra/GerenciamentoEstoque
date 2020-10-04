using GerenciamentoEstoque.Models.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Repositories.Produto
{
    public interface IProdutoRepository
    {
        public void InserirProduto(ProdutoVD Produto);
        public void EditarProduto(ProdutoVD Produto);
        public void RemoverProduto(int codProduto);
        public List<ProdutoVD> CarregarListaProduto();
        public void AtualizarPrecoCustoMedioProduto(int codProduto, double custoUnitarioMovimentacao, int qtdMovimentada);
    }
}
