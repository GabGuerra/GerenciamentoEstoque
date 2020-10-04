using GerenciamentoEstoque.Models.Filial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.LocalFisico
{
    public class LocalFisicoVD
    {
        public int CodLocalFisico { get; set; }
        public string NomeLocalFisico { get; set; }
        public FilialVD Filial { get; set; }
        public LocalFisicoVD()
        {
            Filial = new FilialVD();
        }
    }
}
