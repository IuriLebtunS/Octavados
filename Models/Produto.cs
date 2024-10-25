using System.ComponentModel.DataAnnotations;

namespace Octavados.Models;


public class Produto
{
    public Produto() { }
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public string Marca { get; set; }
    public string ImagemUrl { get; set; }
    public int EstoqueId { get; set; }
    public Estoque Estoque { get; set; }
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "O campo Categoria é obrigatório.")]
    public Categoria Categoria { get; set; }
}