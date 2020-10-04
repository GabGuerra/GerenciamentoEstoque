using GerenciamentoEstoque.Models.Cliente;
using GerenciamentoEstoque.Models.Movimentacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Documento
{
    public class DocumentoVD
    {
        public int? CodDocumento { get; set; }
        public ClienteVD Cliente { get; set; }
        public List<MovimentacaoVD> ListaMovimentacao { get; set; }
        public DocumentoVD()
        {
            Cliente = new ClienteVD();
        }        
    }
}
