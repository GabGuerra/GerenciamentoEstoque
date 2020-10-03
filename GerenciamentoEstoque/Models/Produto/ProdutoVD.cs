﻿using GerenciamentoEstoque.Models.UnidadeMedida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Produto
{
    public class ProdutoVD : IProduto
    {
        public int CodProduto { get; set; }
        public string NomeProduto { get; set; }
        public double PrecoCustoMedio { get; set; }
        public UnidadeMedidaVD UnidadeMedida { get; set; }
    }
}
