using Microsoft.AspNetCore.Mvc.Rendering;
using Octavados.Models;

namespace Octavados.ViewModels
{
    public class CriarVendaViewModel
    {
        public int ClienteId { get; set; }  
        public DateTime DataVenda { get; set; }  
        public decimal ValorDoFrete { get; set; }  
        public List<Produto> Produtos { get; set; } = new List<Produto>();
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

