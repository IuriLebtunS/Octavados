using System.ComponentModel.DataAnnotations;
namespace Octavados.ViewModels
{
    public class CriarVendaViewModel
    {
        public int ClienteId { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorDoFrete { get; set; }
        public List<ProdutoVendaItem> ProdutosVenda { get; set; } = new List<ProdutoVendaItem>();

    }

    public class ProdutoVendaItem
    {
        public int ProdutoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero.")]
        public decimal PrecoUnitario { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O desconto não pode ser negativo.")]
        public decimal Desconto { get; set; }
    }
}

