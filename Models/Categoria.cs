namespace Octavados.Models;

public class Categoria
{
    public Categoria(){}
    
    public int Id {get; set;}
    public string Nome{ get; set;}
    public List<Produto> Produto{get; set;}

}