using GerenciamentoEstoque.Enums;
using GerenciamentoEstoque.Models.Cliente;
using GerenciamentoEstoque.Models.Deposito;
using GerenciamentoEstoque.Models.Documento;
using GerenciamentoEstoque.Models.Filial;
using GerenciamentoEstoque.Models.LocalFisico;
using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoEstoque.Models.MovimentacaoDetalhe;
using GerenciamentoEstoque.Models.Produto;
using GerenciamentoEstoque.Repositories.BD;
using GerenciamentoEstoque.Repositories.Movimentacao;
using GerenciamentoIdentidadeCore2.Models;
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
        public ResultadoVD MovimentarProdutos(MovimentacaoVD movimentacao)
        {
            ResultadoVD resultado = new ResultadoVD(true);
            resultado.Sucesso = InserirMovimentacao(movimentacao) > 0;
            if (resultado.Sucesso)
            {
                foreach (var item in movimentacao.ListaMovimentacaoDetalhe)
                    InserirMovimentacaoDetalhe(item);
            }
            return resultado;
        }

        public int InserirMovimentacao(MovimentacaoVD mov)
        {

            var sql = @"INSERT INTO MOVIMENTACAO    
                            (
                             COD_CLIENTE,
                             DAT_MOVIMENTACAO,
                             COD_TIPO_MOVIMENTACAO
                            ) 
                        VALUES
                            (
                             @COD_CLIENTE,
                             CURDATE(),
                             @COD_TIPO_MOVIMENTACAO
                            )";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("COD_CLIENTE", mov.Cliente.CodCliente);
                cmd.Parameters.AddWithValue("COD_TIPO_MOVIMENTACAO", mov.TipoMovimentacao.CodTipoMovimentacao);

                return ExecutarComando(cmd);
            }

        }

        public void InserirMovimentacaoDetalhe(MovimentacaoDetalheVD movi)
        {
            var sql = @"INSERT INTO MOVIMENTACAO_DETALHE
                            (COD_PRODUTO,
                             COD_DEPOSITO,
                             QTD_MOVIMENTACAO,
                             COD_MOVIMENTACAO)
				        VALUES
                            (@COD_PRODUTO,
                             @COD_DEPOSITO,
                             @QTD_MOVIMENTACAO,
                             (SELECT MAX(M.COD_MOVIMENTACAO) FROM MOVIMENTACAO M)
                            )";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("COD_PRODUTO", movi.Produto.CodProduto);
                cmd.Parameters.AddWithValue("COD_DEPOSITO", movi.Deposito.CodDeposito);
                cmd.Parameters.AddWithValue("QTD_MOVIMENTACAO", movi.QtdMovimentacao);

                ExecutarComando(cmd);
            }
        }   

        public List<MovimentacaoVD> ListarMovimentacoesCliente(int codCliente)
        {
            List<MovimentacaoVD> lista = new List<MovimentacaoVD>();

            string sql = @"
                            SELECT
	                            C.NOME AS 'NOME_CLIENTE',    
                                C.COD_CLIENTE,
                                C.EMAIL AS 'EMAIL_CLIENTE',                        
                                M.DAT_MOVIMENTACAO,
                                M.COD_MOVIMENTACAO,
                                TM.COD_TIPO_MOVIMENTACAO,
                                TM.DSC_TIPO_MOVIMENTACAO                    
                            FROM
                                MOVIMENTACAO M                    
                            INNER JOIN CLIENTE C ON M.COD_CLIENTE = C.COD_CLIENTE                    
                            INNER JOIN TIPO_MOVIMENTACAO TM ON M.COD_TIPO_MOVIMENTACAO = TM.COD_TIPO_MOVIMENTACAO
                            WHERE
                                C.COD_CLIENTE = @COD_CLIENTE";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@COD_CLIENTE", codCliente);
                lista = ObterRegistros(cmd).ToList();
            }

            return lista;
        }

        public MovimentacaoVD GerarNotaFiscal(int codMovimentacao)
        {
            MovimentacaoVD movimentacao = new MovimentacaoVD();

            string sql = @"SELECT
	                            C.NOME AS 'NOME_CLIENTE',
                                TDI.DSC_TIPO_DOCUMENTO_IDENTIFICACAO,
                                DIC.NUMERO_DOCUMENTO,
                                P.COD_PRODUTO,
	                            P.NOME_PRODUTO,
                                MD.QTD_MOVIMENTACAO,
                                M.DAT_MOVIMENTACAO,
                                TM.DSC_TIPO_MOVIMENTACAO,
                                F.NOME_FILIAL,    
                                LF.NOME_LOCAL_FISICO,                                    
                                D.NOME_DEPOSITO                                
                           FROM
	                            MOVIMENTACAO_DETALHE MD
                           INNER JOIN MOVIMENTACAO M ON MD.COD_MOVIMENTACAO = M.COD_MOVIMENTACAO
                           INNER JOIN TIPO_MOVIMENTACAO TM ON M.COD_TIPO_MOVIMENTACAO = TM.COD_TIPO_MOVIMENTACAO
                           INNER JOIN PRODUTO_DEPOSITO PD ON (MD.COD_DEPOSITO = PD.COD_DEPOSITO AND MD.COD_PRODUTO = PD.COD_PRODUTO)
                           INNER JOIN PRODUTO P ON PD.COD_PRODUTO = P.COD_PRODUTO
                           INNER JOIN DEPOSITO D ON PD.COD_DEPOSITO = D.COD_DEPOSITO
                           INNER JOIN LOCAL_FISICO LF ON D.COD_LOCAL_FISICO = LF.COD_LOCAL_FISICO
                           INNER JOIN FILIAL F ON LF.COD_FILIAL = F.COD_FILIAL
                           LEFT JOIN CLIENTE C ON M.COD_CLIENTE = C.COD_CLIENTE
                           LEFT JOIN DOCUMENTO_IDENTIFICACAO_CLIENTE DIC ON C.COD_CLIENTE = DIC.COD_CLIENTE
                           LEFT JOIN TIPO_DOCUMENTO_IDENTIFICACAO TDI ON DIC.COD_TIPO_DOCUMENTO_IDENTIFICACAO = TDI.COD_TIPO_DOCUMENTO_IDENTIFICACAO
                           WHERE
	                            MD.COD_MOVIMENTACAO = @COD_MOVIMENTACAO";

            using (var cmd = new MySqlCommand(sql))
            {
                try
                {
                    cmd.Connection = _conn;
                    _conn.Open();
                    cmd.Parameters.AddWithValue("@COD_MOVIMENTACAO", codMovimentacao);
                    using (var dr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (dr.Read())
                            {
                                movimentacao.Cliente.NomeCliente = dr["NOME_CLIENTE"].ToString();
                                movimentacao.TipoMovimentacao.DscTipoMovimentacao = dr["DSC_TIPO_MOVIMENTACAO"].ToString();
                                movimentacao.Cliente.Documento.NumeroDocumento = dr["NUMERO_DOCUMENTO"].ToString();
                                movimentacao.Cliente.Documento.TipoDocumento.DscTipoDocumentoIdentificacao = dr["DSC_TIPO_DOCUMENTO_IDENTIFICACAO"].ToString();
                                movimentacao.ListaMovimentacaoDetalhe.Add(new MovimentacaoDetalheVD
                                {
                                    Produto = new ProdutoVD
                                    {
                                        NomeProduto = dr["NOME_PRODUTO"].ToString(),
                                        CodProduto = Convert.ToInt32(dr["COD_PRODUTO"])
                                    },
                                    Deposito = new DepositoVD
                                    {
                                        NomeDeposito = dr["NOME_DEPOSITO"].ToString(),
                                        LocalFisico = new LocalFisicoVD
                                        {
                                            NomeLocalFisico = dr["NOME_LOCAL_FISICO"].ToString(),
                                            Filial = new FilialVD
                                            {
                                                NomeFilial = dr["NOME_FILIAL"].ToString()
                                            }
                                        }

                                    },
                                    QtdMovimentacao = Convert.ToInt32(dr["QTD_MOVIMENTACAO"])
                                });
                                movimentacao.DatMovimentacao = Convert.ToDateTime(dr["DAT_MOVIMENTACAO"]);
                                movimentacao.TipoMovimentacao.DscTipoMovimentacao = dr["DSC_TIPO_MOVIMENTACAO"].ToString();
                            }
                        }
                        finally { dr.Close(); }
                    }
                }
                finally { _conn.Close(); }
            }

            return movimentacao;
        }

        public override MovimentacaoVD PopularDados(MySqlDataReader dr)
        {
            var mov = new MovimentacaoVD
            {
                Cliente = new ClienteVD(Convert.ToInt32(dr["COD_CLIENTE"]), dr["NOME_CLIENTE"].ToString(), dr["EMAIL_CLIENTE"].ToString()),
                DatMovimentacao = Convert.ToDateTime(dr["DAT_MOVIMENTACAO"]),
                CodMovimentacao = Convert.ToInt32(dr["COD_MOVIMENTACAO"]),
                TipoMovimentacao = new TipoMovimentacaoVD(Convert.ToInt32(dr["COD_TIPO_MOVIMENTACAO"]), dr["DSC_TIPO_MOVIMENTACAO"].ToString())
            };

            return mov;
        }
    }
}
