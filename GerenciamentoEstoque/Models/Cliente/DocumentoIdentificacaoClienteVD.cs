using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Cliente
{
    public class DocumentoIdentificacaoClienteVD
    {
        public string NumeroDocumento { get; set; }
        public TipoDocumentoIdentificacaoCliente TipoDocumento { get; set; }
        public DocumentoIdentificacaoClienteVD()
        {
            TipoDocumento = new TipoDocumentoIdentificacaoCliente();
        }
    }
}
