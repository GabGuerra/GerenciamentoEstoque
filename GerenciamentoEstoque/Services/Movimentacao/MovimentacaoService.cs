using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoEstoque.Models.Documento;
using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoEstoque.Repositories.Movimentacao;
using GerenciamentoIdentidadeCore2.Models;

namespace GerenciamentoEstoque.Services.Movimentacao
{
    public class MovimentacaoService : IMovimentacaoService
    {
        public IMovimentacaoRepository _movimentacaoRepository { get; set; }
        public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
        }
        public ResultadoVD MovimentarProdutos(DocumentoVD Movimentacao)
        {
            ResultadoVD resultado = new ResultadoVD();
            try
            {
                resultado = _movimentacaoRepository.MovimentarProdutos(Movimentacao);
                resultado.Mensagem = resultado.Sucesso ? "Movimentação concluída com sucesso." : "Não foi possível concluir a movimentação.";
            }
            catch (Exception ex)
            {
                resultado.Mensagem = $"Não foi possível concluir a movimentação. {Environment.NewLine} {ex.Message}";
                resultado.Sucesso = false;
            }

            return resultado;
        }

        public ResultadoVD ListarMovimentacoesCliente(int codCliente) 
        {
            ResultadoVD resultado = new ResultadoVD(true);
            try
            {
                resultado.Resultado = _movimentacaoRepository.ListarMovimentacoesCliente(codCliente);
            }
            catch (Exception ex)
            {
                resultado.Mensagem = $"Não foi possível listar as movimentações do cliente. {Environment.NewLine} {ex.Message}";
                resultado.Sucesso = false;
            }
            return resultado;
        }

        public ResultadoVD GerarNotaFiscal(int codDocumento)
        {

            ResultadoVD resultado = new ResultadoVD(true);
            try
            {
                resultado.Resultado = FormatarNotaFiscal(_movimentacaoRepository.GerarNotaFiscal(codDocumento));
            }
            catch (Exception ex)
            {
                resultado.Sucesso = false;
                resultado.Mensagem = $"Não foi possível gerar a nota fiscal {Environment.NewLine} {ex.Message}";
            }
            return resultado;
        }


        public string FormatarNotaFiscal(DocumentoVD nota) 
        {
            var sb = new StringBuilder();
            sb.AppendLine($"---------NOTA FISCAL DE {nota.TipoDocumento.DscTipoDocumento.ToUpper()}-----------");
            sb.AppendLine($"Data:  {nota.DatMovimentacao.Value.ToLocalTime()}");
            sb.AppendLine($"Cliente: {nota.Cliente.NomeCliente}");
            sb.AppendLine($"{nota.Cliente.Documento.TipoDocumento.DscTipoDocumentoIdentificacao}:  {nota.Cliente.Documento.NumeroDocumento}");            

            foreach (var item in nota.ListaMovimentacaoDetalhe)
            {
                string endereco =  $"Deposito: {item.Deposito.NomeDeposito} | Local físico: {item.Deposito.LocalFisico.NomeLocalFisico} | Filial: {item.Deposito.LocalFisico.Filial.NomeFilial}";
                sb.AppendLine($"______________________________________________________________________");
                sb.AppendLine($"Produto: {item.Produto.NomeProduto} | QTD: {item.QtdMovimentacao} | {endereco}");                
            }

            sb.AppendLine($"DOCUMENTO GERADO EM: {DateTime.Now.ToLocalTime()}");
            return sb.ToString();
        }
    }
}
