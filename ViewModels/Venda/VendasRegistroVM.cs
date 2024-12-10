using Microsoft.AspNetCore.Mvc.Rendering;

namespace Octavados.ViewModels
{
    public class CriarVendaViewModel
    {
        public int ClienteId { get; set; }  
        public DateTime DataVenda { get; set; }  
        public decimal ValorDoFrete { get; set; }  
        public List<SelectListItem> Produtos { get; set; }
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

