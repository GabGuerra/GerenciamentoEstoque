using GerenciamentoEstoque.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Documento
{
    public class DocumentoVD
    {
        public int CodDocumento { get; set; }
        public ClienteVD Cliente { get; set; }


    }
}
