using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Movimentacao
{
    public class TipoDocumentoVD
    {
        public int CodTipoDocumento { get; set; }
        public string DscTipoDocumento { get; set; }
        public TipoDocumentoVD()
        {

        }
        public TipoDocumentoVD(int codTipoDocumento, string dscTipoDocumento)
        {
            CodTipoDocumento = codTipoDocumento;
            DscTipoDocumento = dscTipoDocumento;
        }
        public TipoDocumentoVD(string dscTipoDocumento)
        {
            DscTipoDocumento = dscTipoDocumento;
        }
    }
}
