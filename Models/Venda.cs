namespace Octavados.Models;

public class Venda
{
    public Venda(){}
    public int Id { get; set; }
    public Detalhe Detalhe { get; set; }
    public decimal Total { get; set; }
    public decimal Desconto { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorDoFrete { get; set;} 
}