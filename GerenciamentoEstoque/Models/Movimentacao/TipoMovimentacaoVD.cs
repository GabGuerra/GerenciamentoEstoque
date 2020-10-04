using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Movimentacao
{
    public class TipoMovimentacaoVD
    {
        public int CodTipoMovimentacao { get; set; }
        public string DscTipoMovimentacao { get; set; }
        public TipoMovimentacaoVD()
        {

        }
        public TipoMovimentacaoVD(int codTipoMovimentacao, string dscTipoMovimentacao)
        {
            CodTipoMovimentacao = codTipoMovimentacao;
            DscTipoMovimentacao = dscTipoMovimentacao;
        }
        public TipoMovimentacaoVD(string dscTipoMovimentacao)
        {
            DscTipoMovimentacao = dscTipoMovimentacao;
        }
    }
}
