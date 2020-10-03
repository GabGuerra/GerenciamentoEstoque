using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Endereco
{
    public interface IEndereco
    {
        public int CodEndereco { get; set; }
        public string NomeRua { get; set; }
        public int Cep { get; set; }
    }
}
