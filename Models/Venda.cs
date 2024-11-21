namespace Octavados.Models;

public class Venda
{
    public Venda() { }
  
        public int Id { get; set; }
        public List<DetalheDaVenda> DetalhesDaVenda { get; set; } = new List<DetalheDaVenda>();
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public decimal ValorDoFrete { get; set; }
        public decimal Total => DetalhesDaVenda.Sum(d => d.Total) + ValorDoFrete;
    
    public class DetalheDaVenda
    {
        public int Id { get; set; } 
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; } 
        public decimal PrecoUnitario { get; set; } 
        public decimal Desconto { get; set; }
        public decimal Total => (Quantidade * PrecoUnitario) - Desconto;
    }
}




