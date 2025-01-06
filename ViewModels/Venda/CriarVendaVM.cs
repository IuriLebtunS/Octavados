public class CriarVendaViewModel
{
    public int ClienteId { get; set; }
    public DateTime DataVenda { get; set; }
    public decimal ValorDoFrete { get; set; }

    public List<ProdutoVendaItem> ProdutosVenda { get; set; } = new List<ProdutoVendaItem>();
}
