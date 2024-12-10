using Microsoft.AspNetCore.Mvc.Rendering;

namespace Octavados.ViewModels
{
    public class CriarVendaViewModel
    {
        public int ClienteId { get; set; }  // Cliente relacionado à venda
        public DateTime DataVenda { get; set; }  // Data da venda
        public decimal ValorDoFrete { get; set; }  // Valor do frete

        // Lista de produtos disponíveis para a venda
        public List<SelectListItem> Produtos { get; set; }

        // Lista de produtos que o usuário está selecionando para a venda
        public List<ProdutoVendaItem> ProdutosVenda { get; set; } = new List<ProdutoVendaItem>();
    }

    public class ProdutoVendaItem
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
    }
}

