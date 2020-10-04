

using System.ComponentModel;

namespace GerenciamentoEstoque.Enums
{
    public enum EnumTipoDocumento
    {
        [Description("Entrada por compra")]
        EntradaPorCompra = 1,
        [Description("Saída por venda")]
        SaidaPorVenda = 2,
        [Description("Saída para consumo interno")]
        SaidaConsumoInterno = 3,
        [Description("Saída do estoque para fabricação")]
        SaidaEstoqueParaFabricacao = 4,
        [Description("Saída do estoque para devolução")]
        SaidaDevolucao = 5,
        [Description("Saída por perda")]
        SaidaPorPerda = 6

    }
}
