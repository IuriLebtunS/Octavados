namespace LojaOctavados.Models;

public class Produto
{
    public Produto(){}

    public int Id { get; set;}
    public string Nome { get; set;}
    public decimal Preco { get ; set;}
    public string Marca { get; set;}

    public int QuantidadeEmEstoque { get; set;}
    public string Imagem { get; set;}
    public int CategoriaId { get; set;}

    public Categoria Categoria { get; set;}
}