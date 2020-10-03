using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoEstoque.Services.Movimentacao;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Controllers.Movimentacao
{
    public class MovimentacaoController:Controller
    {
        public ImovimentacaoService _movimentacaoService;
        public MovimentacaoController(ImovimentacaoService movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }

        public JsonResult MovimentarProduto (MovimentacaoVD Movimentacao)
        {
            return Json(_movimentacaoService.MovimentarProduto(Movimentacao));
        }
    }
}
