using GerenciamentoEstoque.Models.Deposito;
using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoEstoque.Models.Produto;
using GerenciamentoEstoque.Repositories.BD;
using GerenciamentoEstoque.Repositories.Movimentacao;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Repositories.Movimentacao
{
    public class MovimentacaoRepository : MySqlRepository<MovimentacaoVD>, IMovimentacaoRepository
    {
        public MovimentacaoRepository(IConfiguration config) : base(config)
        {
        }

        // INSERT NA TABELA MOVIMENTACAO_DETALHE
        public MovimentacaoVD MovimentarProduto(Imovimentacao movimentacao)
        {
            var sql = @"INSERT INTO DOCUMENTO (COD_CLIENTE) VALUES (@COD_CLIENTE)";
            using (var command = new MySqlCommand(sql))
            {
                command.Parameters.AddWithValue("COD_CLIENTE", movimentacao.Documento.Cliente.CodCliente);
                ExecutarComando(command);
                //insere documentacao_detalhe
                sql = @"INSERT INTO MOVIMENTACAO_DETALHE
                    (COD_PRODUTO, COD_DEPOSITO, QTD_MOVIMENTACAO,DAT_MOVIMENTACAO,COD_TIPO_MOVIMENTACAO, COD_DOCUMENTO)
				                    VALUES
                    (@COD_PRODUTO, @COD_DEPOSITO, @QTD_MOVIMENTACAO, CURRENT_DATE, @COD_TIPO_MOVIMENTACAO, (SELECT AUTO_INCREMENT-1
                                                                                                            FROM information_schema.TABLES
                                                                                                            WHERE TABLE_SCHEMA = 'ESTOQUE'
                                                                                                            AND TABLE_NAME = 'DOCUMENTO')); ";
                command.Parameters.Clear();
                command.CommandText = sql;
                command.Parameters.AddWithValue("COD_PRODUTO", movimentacao.Produto.CodProduto);
                command.Parameters.AddWithValue("COD_DEPOSITO", movimentacao.Deposito.CodDeposito);
                command.Parameters.AddWithValue("QTD_MOVIMENTACAO", movimentacao.QtdMovimentacao);
                command.Parameters.AddWithValue("COD_TIPO_MOVIMENTACAO", movimentacao.TipoMovimentacao.CodTipoMovimentacao);
                command.Parameters.AddWithValue("COD_DOCUMENTO", movimentacao.Documento.CodDocumento);
                ExecutarComando(command);
                sql = @"select  
                               CONCAT('NF - E: ', tm.nome_tipo_movimentacao) TipoNF,
                               CONCAT('CLIENTE: ', cli.nome) NomCliente,
                               CONCAT('DEPOSITO: ', dep.nome_deposito) Deposito,
                               CONCAT('PRODUTO: ', P.NOME_PRODUTO) Produto,
                               /*CONCAT('QUANTIDADE: ', MD.QTD_MOVIMENTACAO) Quantidade*/
                               MD.QTD_MOVIMENTACAO Quantidade,
                               /*CONCAT('DATA: ', MD.DAT_MOVIMENTACAO) DataMovimento*/
                               MD.DAT_MOVIMENTACAO DataMovimento
                            from movimentacao_detalhe md
                            inner
                            join tipo_movimentacao tm on md.cod_tipo_movimentacao = tm.cod_tipo_movimentacao
                            inner
                            join produto p on md.cod_produto = p.cod_produto
                            inner
                            join deposito dep on md.cod_deposito = dep.cod_deposito
                            inner
                            join documento doc on md.cod_documento = doc.cod_documento
                            inner
                            join cliente cli on doc.cod_cliente = cli.cod_cliente
                            where MD.COD_DOCUMENTO = (SELECT AUTO_INCREMENT - 1
                                                                                                                                        FROM information_schema.TABLES
                                                                                                                                        WHERE TABLE_SCHEMA = 'ESTOQUE'
                                                                                                                                        AND TABLE_NAME = 'DOCUMENTO');";
                command.Parameters.Clear();
                command.CommandText = sql;
                return ObterRegistro(command);

            }
        }

        public override MovimentacaoVD PopularDados(MySqlDataReader dr)
        {
            var movimentacao = new MovimentacaoVD();

            movimentacao.TipoMovimentacao.NomeTipoMovimentacao = dr["TipoNF"].ToString();
            movimentacao.Produto.NomeProduto = dr["Produto"].ToString();
            movimentacao.Deposito.NomeDeposito = dr["Deposito"].ToString();
            movimentacao.QtdMovimentacao = Convert.ToInt32(dr["Quantidade"]);
            movimentacao.DatMovimentacao = Convert.ToDateTime(dr["DataMovimento"]);
            return movimentacao;
        }
    }
}
