﻿using GerenciamentoEstoque.Models.Produto;
using GerenciamentoEstoque.Repositories.Produto;
using GerenciamentoIdentidadeCore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Services.Produto
{
    public class ProdutoService : IProdutoService
    {
        public IProdutoRepository _produtoRepository { get; set; }
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public ResultadoVD InserirProduto(ProdutoVD produto)
        {
            ResultadoVD resultado = new ResultadoVD(true);

            try
            {
                _produtoRepository.InserirProduto(produto);
            }
            catch (Exception ex)
            {
                resultado.Mensagem = ex.Message;
                resultado.Sucesso = false;
            }

            return resultado;
        }

        public List<ProdutoVD> CarregarListaProduto()
        {
            return _produtoRepository.CarregarListaProduto();
        }

        public ResultadoVD EditarProduto(ProdutoVD produto)
        {
            ResultadoVD resultado = new ResultadoVD(true);

            try
            {
                _produtoRepository.EditarProduto(produto);
            }
            catch (Exception ex)
            {
                resultado.Mensagem = ex.Message;
                resultado.Sucesso = false;
            }

            return resultado;
        }
        
        public ResultadoVD RemoverProduto(int codProduto)
        {
            ResultadoVD resultado = new ResultadoVD(true);

            try
            {
                _produtoRepository.RemoverProduto(codProduto);
            }
            catch (Exception ex)
            {
                resultado.Mensagem = ex.Message;
                resultado.Sucesso = false;
            }

            return resultado;
        }

        public void AtualizarPrecoCustoMedioProduto(int codProduto, double custoUnitarioMovimentacao, int qtdMovimentada) 
        {
            _produtoRepository.AtualizarPrecoCustoMedioProduto(codProduto, custoUnitarioMovimentacao, qtdMovimentada);
        } 
    }
}
