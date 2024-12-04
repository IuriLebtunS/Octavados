namespace Octavados.Models;

public class Venda
{
    public Venda() { }

    public int Id { get; set; }
    public List<ProdutoVenda> ProdutosVenda { get; set; } = new List<ProdutoVenda>();  // Atualizado o nome da lista para refletir os produtos da venda
    public DateTime DataVenda { get; set; } = DateTime.Now;
    public decimal ValorDoFrete { get; set; }
    public decimal Total => ProdutosVenda.Sum(d => d.Total) + ValorDoFrete;
    public int ClienteId { get; set; } 
    public Cliente Cliente { get; set; } 
}

public class ProdutoVenda
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Desconto { get; set; }
    public decimal Total => (Quantidade * PrecoUnitario) - Desconto;
}




