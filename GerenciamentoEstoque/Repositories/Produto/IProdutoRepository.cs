using GerenciamentoEstoque.Models.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Repositories.Produto
{
    public interface IProdutoRepository
    {
        public void InserirProduto(IProduto Produto);
        public void EditarProduto(IProduto Produto);
        public void RemoverProduto(IProduto Produto);
        public List<ProdutoVD> CarregarListaProduto();
    }
}
