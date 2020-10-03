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
        public ResultadoVD InserirProduto(IProduto Produto);
        public ResultadoVD EditarProduto(IProduto Produto);
        public ResultadoVD RemoverProduto(IProduto Produto);
        public List<ProdutoVD> CarregarListaProduto();

    }
}
