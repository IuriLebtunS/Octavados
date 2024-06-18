namespace Octavados.Models;

public class Categoria
{
    public Categoria(){}
    
    public int Id {get; set;}
    public string Nome{ get; set;}
    public bool  Ativo {get; set;}
    public List<Produto> Produtos{get; set;}

}