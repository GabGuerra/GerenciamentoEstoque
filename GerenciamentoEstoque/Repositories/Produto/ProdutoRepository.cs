using GerenciamentoEstoque.Models.Produto;
using GerenciamentoEstoque.Repositories.BD;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Repositories.Produto
{
    public class ProdutoRepository : MySqlRepository<ProdutoVD>, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration config) : base(config)
        {
        }

        public void InserirProduto(ProdutoVD produto)
        {
            var sql = @"INSERT INTO PRODUTO
                        (NOME_PRODUTO,COD_FORNECEDOR,COD_UNIDADE_MEDIDA) 
                                                    VALUES
                       (@NOME_PRODUTO,@COD_FORNECEDOR,@COD_UNIDADE_MEDIDA);";
            using (var command = new MySqlCommand(sql))
            {
                command.Parameters.AddWithValue("NOME_PRODUTO", produto.NomeProduto);
                command.Parameters.AddWithValue("COD_FORNECEDOR", produto.Fornecedor.CodFornecedor); 
                command.Parameters.AddWithValue("COD_UNIDADE_MEDIDA", produto.UnidadeMedida.CodUnidadeMedida);
                ExecutarComando(command);
            }
        }
        public void EditarProduto(ProdutoVD produto)
        {
            var sql = @"UPDATE PRODUTO M SET 
                        M.NOME_PRODUTO = @NOME_PRODUTO
                        ,M.PRECO_CUSTO_MEDIO= @PRECO_CUSTO_MEDIO, 
                        WHERE M.COD_PRODUTO = @COD_PRODUTO";
            using (var command = new MySqlCommand(sql))
            {
                command.Parameters.AddWithValue("NOME_PRODUTO", produto.NomeProduto);
                command.Parameters.AddWithValue("COD_PRODUTO", produto.CodProduto);
                command.Parameters.AddWithValue("PRECO_CUSTO_MEDIO", produto.PrecoCustoMedio);
                ExecutarComando(command);
            }
        }
        public void RemoverProduto(int codProduto)
        {
            var sql = @"DELETE FROM PRODUTO WHERE COD_PRODUTO = @COD_PRODUTO";
            using (var command = new MySqlCommand(sql))
            {
                command.Parameters.AddWithValue("COD_PRODUTO", codProduto);
                ExecutarComando(command);
            }
        }
        public List<ProdutoVD> CarregarListaProduto()
        {
            List<ProdutoVD> lista = new List<ProdutoVD>();
            var sql = @"SELECT
	                        COD_PRODUTO,
                            NOME_PRODUTO                            
                        FROM
	                        PRODUTO";
            using (var cmd = new MySqlCommand(sql))
            {
                lista = ObterRegistros(cmd).ToList();
            }

            return lista;
        }

             public void AtualizarPrecoCustoMedioProduto(int codProduto, double custoUnitarioMovimentacao, int qtdMovimentada) 
        {
            var sql = @"CALL PROC_ATUALIZA_PRECO_MED_PRODUTO(@P_COD_PRODUTO, @P_CUSTO_UNITARIO_MOV_ATUAL, @P_QTD_MOV_ATUAL)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@P_COD_PRODUTO", codProduto);
                cmd.Parameters.AddWithValue("@P_CUSTO_UNITARIO_MOV_ATUAL", custoUnitarioMovimentacao);
                cmd.Parameters.AddWithValue("@P_QTD_MOV_ATUAL", qtdMovimentada);

                ExecutarComando(cmd);
            }
        }

        public override ProdutoVD PopularDados(MySqlDataReader dr)
        {
            return new ProdutoVD
            {
                CodProduto = Convert.ToInt32(dr["COD_PRODUTO"]),
                NomeProduto = dr["NOME_PRODUTO"].ToString()
            };
        }
    }
}

