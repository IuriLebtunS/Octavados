namespace Octavados.Models;

public class Venda
{
    public Venda() { }

    public int Id { get; set; }
    public List<ProdutoVenda> ProdutosVenda { get; set; } = new List<ProdutoVenda>(); 
    public DateTime DataVenda { get; set; } = DateTime.Now;
    public decimal ValorDoFrete { get; set; }
    public decimal Total => ProdutosVenda.Sum(d => d.TotalDaVenda) + ValorDoFrete;
    public int ClienteId { get; set; } 
    public Cliente Cliente { get; set; } 
}

public class ProdutoVenda
{
    public int Id { get; set; }
    public Produto Produto { get; set; } 
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Desconto { get; set; }
    public decimal TotalDaVenda => (Quantidade * PrecoUnitario) - Desconto;
}




