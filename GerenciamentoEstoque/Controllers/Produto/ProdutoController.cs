using GerenciamentoEstoque.Models.Produto;
using GerenciamentoEstoque.Services.Produto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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

        [HttpPost]
        public JsonResult InserirProduto(ProdutoVD produto)
        {
            return Json(_produtoService.InserirProduto(produto));
        }

        [HttpDelete]
        public JsonResult RemoverProduto(int codProduto)
        {
            return Json(_produtoService.RemoverProduto(codProduto));
        }

        public IActionResult GridProdutos()
        {
            return PartialView("GridProdutos");
        }
    }
}
