using GerenciamentoEstoque.Models.Produto;
using GerenciamentoEstoque.Services.Produto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque
{
    public class ProdutoController : Controller
    {

        public IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public IActionResult Index()
        {
            ViewBag.ListaProdutos = _produtoService.CarregarListaProduto();
            return View("ProdutoIndex");
        }

        public JsonResult InserirProduto(ProdutoVD produto)
        {
            return Json(_produtoService.InserirProduto(produto));
        }

        public JsonResult EditarProduto(ProdutoVD produto)
        {
            return Json(_produtoService.EditarProduto(produto));
        }
        public JsonResult RemoverProduto(ProdutoVD produto)
        {

            return Json(_produtoService.RemoverProduto(produto));
        }

        public IActionResult GridProdutos()
        {
            return PartialView("GridProdutos");
        }
    }
}
