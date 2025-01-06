public class AdicionarProdutoViewModel
{
    public int VendaId { get; set; }
    public List<ProdutoVendaItem> ProdutosVenda { get; set; } = new List<ProdutoVendaItem>();
}

public class ProdutoVendaItem
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal? Desconto { get; set; }
}
