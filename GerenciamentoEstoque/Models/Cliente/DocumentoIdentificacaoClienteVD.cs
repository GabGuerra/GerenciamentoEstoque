using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Cliente
{
    public class DocumentoIdentificacaoClienteVD
    {
        public int CodDocumentoIdentificacao { get; set; }
        public string NumeroDocumento { get; set; }
        public ClienteVD Cliente { get; set; }
        public TipoDocumentoIdentificacaoCliente TipoDocumento {get;set ;}
    }
}
