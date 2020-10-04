using GerenciamentoEstoque.Enums;
using GerenciamentoEstoque.Models.Cliente;
using GerenciamentoEstoque.Models.MovimentacaoDetalhe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Movimentacao
{
    public class MovimentacaoVD
    {
        public int? CodMovimentacao { get; set; }
        public DateTime? DatMovimentacao { get; set; }
        public TipoMovimentacaoVD TipoMovimentacao { get; set; }
        public ClienteVD Cliente { get; set; }
        public List<MovimentacaoDetalheVD> ListaMovimentacaoDetalhe { get; set; }
        public MovimentacaoVD()
        {
            Cliente = new ClienteVD();
            ListaMovimentacaoDetalhe = new List<MovimentacaoDetalheVD>();
            TipoMovimentacao = new TipoMovimentacaoVD();

        }
    }
}
