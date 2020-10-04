using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Models.Cliente
{
    public class ClienteVD
    {
        public int CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string Email { get; set; }
        public DocumentoIdentificacaoClienteVD Documento { get; set; }
        public ClienteVD()
        {
            Documento = new DocumentoIdentificacaoClienteVD();
        }
        public ClienteVD(int codCliente, string nomeCliente, string email)
        {
            CodCliente = codCliente;
            NomeCliente = nomeCliente;
            Email = email;
        }
    }
}
