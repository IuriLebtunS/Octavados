namespace Octavados.Models;

public class Detalhe
{
    public Detalhe(){}

    public int Id { get; set;}

    public Produto Produto { get; set;}
    public decimal Total { get; set;}
    
}