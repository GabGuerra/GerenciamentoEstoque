using GerenciamentoEstoque.Enums;
using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoEstoque.Services.Movimentacao;
using GerenciamentoEstoque.Services.Produto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Controllers.Movimentacao
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovimentacaoController : Controller
    {
        public IMovimentacaoService _movimentacaoService;
        public IProdutoService _produtoService;
        public MovimentacaoController(IMovimentacaoService movimentacaoService, IProdutoService produtoService)
        {
            _movimentacaoService = movimentacaoService;
            _produtoService = produtoService;
        }

        [HttpPost]
        public JsonResult MovimentarProdutos(MovimentacaoVD mov)
        {
            if (mov.TipoMovimentacao.CodTipoMovimentacao == EnumTipoMovimentacao.EntradaPorCompra.GetHashCode())
            {
                foreach (var item in mov.ListaMovimentacaoDetalhe)
                    _produtoService.AtualizarPrecoCustoMedioProduto(item.Produto.CodProduto.Value, item.PrecoUnitarioMovimentacao, item.QtdMovimentacao);
            }
            return Json(_movimentacaoService.MovimentarProdutos(mov));
        }


        [HttpGet]
        public JsonResult ListarMovimentacoesCliente(int codCliente)
        {
            return Json(_movimentacaoService.ListarMovimentacoesCliente(codCliente));
        }

        [HttpGet]
        public JsonResult GerarNotaFiscal(int codMovimentacao)
        {
            return Json(_movimentacaoService.GerarNotaFiscal(codMovimentacao).Resultado);
        }
    }
}
