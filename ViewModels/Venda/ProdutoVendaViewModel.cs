namespace Octavados.ViewModels;

public class ProdutoVendaViewModel
{
    public int VendaId { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal? Desconto { get; set; }
}
