using GerenciamentoEstoque.Models.LocalFisico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Deposito
{
    public class DepositoVD
    {
        public int CodDeposito { get; set; }
        public string NomeDeposito { get; set; }
        public LocalFisicoVD LocalFisico { get; set; }
        public TipoDepositoVD TipoDeposito { get; set; }
    }
}
