using GerenciamentoEstoque.Models.Deposito;
using GerenciamentoEstoque.Repositories.BD;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoEstoque.Repositories.Deposito
{
    public class DepositoRepository : MySqlRepository<DepositoVD>, IDepositoRepository
    {
        public DepositoRepository(IConfiguration config) : base(config)
        {
        }        
    }
}
