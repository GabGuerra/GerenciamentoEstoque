using GerenciamentoEstoque.Enums;
using GerenciamentoEstoque.Models.Cliente;
using GerenciamentoEstoque.Models.Movimentacao;
using GerenciamentoEstoque.Models.MovimentacaoDetalhe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Documento
{
    public class DocumentoVD
    {
        public int? CodMovimentacao { get; set; }
        public DateTime? DatMovimentacao { get; set; }
        public TipoDocumentoVD TipoDocumento { get; set; }
        public ClienteVD Cliente { get; set; }
        public List<MovimentacaoDetalheVD> ListaMovimentacaoDetalhe { get; set; }
        public DocumentoVD()
        {
            Cliente = new ClienteVD();
            ListaMovimentacaoDetalhe = new List<MovimentacaoDetalheVD>();
            TipoDocumento = new TipoDocumentoVD();
        }
    }
}
