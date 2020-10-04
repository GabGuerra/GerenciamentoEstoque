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
    public class MovimentacaoRepository : MySqlRepository<DocumentoVD>, IMovimentacaoRepository
    {
        public MovimentacaoRepository(IConfiguration config) : base(config)
        {
        }

        // INSERT NA TABELA MOVIMENTACAO_DETALHE
        public ResultadoVD MovimentarProdutos(DocumentoVD movimentacao)
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

        public int InserirMovimentacao(DocumentoVD mov)
        {

            var sql = @"INSERT INTO DOCUMENTO    
                            (
                             COD_CLIENTE,
                             DAT_CRIACAO,
                             COD_TIPO_DOCUMENTO
                            ) 
                        VALUES
                            (
                             @COD_CLIENTE,
                             CURDATE(),
                             @COD_TIPO_DOCUMENTO
                            )";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("COD_CLIENTE", mov.Cliente.CodCliente);
                cmd.Parameters.AddWithValue("COD_TIPO_DOCUMENTO", mov.TipoDocumento.CodTipoDocumento);

                return ExecutarComando(cmd);
            }

        }

        public void InserirMovimentacaoDetalhe(MovimentacaoDetalheVD movi)
        {
            var sql = @"INSERT INTO MOVIMENTACAO_DETALHE
                            (COD_PRODUTO,
                             COD_DEPOSITO,
                             QTD_MOVIMENTACAO,
                             COD_DOCUMENTO)
				        VALUES
                            (@COD_PRODUTO,
                             @COD_DEPOSITO,
                             @QTD_MOVIMENTACAO,
                             (SELECT MAX(D.COD_DOCUMENTO) FROM DOCUMENTO D)
                            )";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("COD_PRODUTO", movi.Produto.CodProduto);
                cmd.Parameters.AddWithValue("COD_DEPOSITO", movi.Deposito.CodDeposito);
                cmd.Parameters.AddWithValue("QTD_MOVIMENTACAO", movi.QtdMovimentacao);

                ExecutarComando(cmd);
            }
        }   

        public List<DocumentoVD> ListarMovimentacoesCliente(int codCliente)
        {
            List<DocumentoVD> lista = new List<DocumentoVD>();

            string sql = @"
                            SELECT
	                            C.NOME AS 'NOME_CLIENTE',    
                                C.COD_CLIENTE,
                                C.EMAIL AS 'EMAIL_CLIENTE',                        
                                D.DAT_MOVIMENTACAO,
                                D.COD_MOVIMENTACAO,
                                TD.COD_TIPO_DOCUMENTO,
                                TD.DSC_TIPO_DOCUMENTO
                            FROM
                                DOCUMENTO D                    
                            INNER JOIN CLIENTE C ON M.COD_CLIENTE = C.COD_CLIENTE                    
                            INNER JOIN TIPO_DOCUMENTO TD ON D.COD_TIPO_DOCUMENTO = TD.COD_TIPO_DOCUMENTO
                            WHERE
                                C.COD_CLIENTE = @COD_CLIENTE";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@COD_CLIENTE", codCliente);
                lista = ObterRegistros(cmd).ToList();
            }

            return lista;
        }

        public DocumentoVD GerarNotaFiscal(int codMovimentacao)
        {
            DocumentoVD movimentacao = new DocumentoVD();

            string sql = @"SELECT
	                            C.NOME AS 'NOME_CLIENTE',
                                TDI.DSC_TIPO_DOCUMENTO_IDENTIFICACAO,
                                DIC.NUMERO_DOCUMENTO,
                                P.COD_PRODUTO,
	                            P.NOME_PRODUTO,
                                MD.QTD_MOVIMENTACAO,
                                M.DAT_MOVIMENTACAO,
                                TD.DSC_TIPO_DOCUMENTO,
                                F.NOME_FILIAL,    
                                LF.NOME_LOCAL_FISICO,                                    
                                D.NOME_DEPOSITO                                
                           FROM
	                            MOVIMENTACAO_DETALHE MD
                           INNER JOIN MOVIMENTACAO M ON MD.COD_MOVIMENTACAO = M.COD_MOVIMENTACAO
                           INNER JOIN TIPO_DOCUMENTO TD ON D.COD_TIPO_DOCUMENTO = TD.COD_TIPO_DOCUMENTO
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
                                movimentacao.TipoDocumento.DscTipoDocumento = dr["DSC_TIPO_DOCUMENTO"].ToString();
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
                                movimentacao.TipoDocumento.DscTipoDocumento = dr["DSC_TIPO_DOCUMENTO"].ToString();
                            }
                        }
                        finally { dr.Close(); }
                    }
                }
                finally { _conn.Close(); }
            }

            return movimentacao;
        }

        public override DocumentoVD PopularDados(MySqlDataReader dr)
        {
            var mov = new DocumentoVD
            {
                Cliente = new ClienteVD(Convert.ToInt32(dr["COD_CLIENTE"]), dr["NOME_CLIENTE"].ToString(), dr["EMAIL_CLIENTE"].ToString()),
                DatMovimentacao = Convert.ToDateTime(dr["DAT_MOVIMENTACAO"]),
                CodMovimentacao = Convert.ToInt32(dr["COD_MOVIMENTACAO"]),
                TipoDocumento = new TipoDocumentoVD(Convert.ToInt32(dr["COD_TIPO_DOCUMENTO"]), dr["DSC_TIPO_DOCUMENTO"].ToString())
            };

            return mov;
        }
    }
}
