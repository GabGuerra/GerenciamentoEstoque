using GerenciamentoEstoque.Enums;
using GerenciamentoEstoque.Models.Cliente;
using GerenciamentoEstoque.Models.Deposito;
using GerenciamentoEstoque.Models.Documento;
using GerenciamentoEstoque.Models.Produto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.MovimentacaoDetalhe
{
    public class MovimentacaoDetalheVD
    {
        public ProdutoVD Produto { get; set; }
        public DepositoVD Deposito { get; set; }
        public int QtdMovimentacao { get; set; }
        public double PrecoUnitarioMovimentacao { get; set; }
        public MovimentacaoDetalheVD()
        {
            Produto = new ProdutoVD();
            Deposito = new DepositoVD();
        }
        public MovimentacaoDetalheVD(ProdutoVD produto, DepositoVD deposito, int qtdMovimentacao)
        {
            Produto = produto;
            Deposito = deposito;
            QtdMovimentacao = qtdMovimentacao;
        }
    }
}
