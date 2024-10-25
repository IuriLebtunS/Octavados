
namespace Octavados.Models
{
  public class Estoque
{
    public int Id { get; set; }
    public string NomeProduto { get; set; }
    public int Quantidade { get; set; }
    public DateTime DataChegada { get; set; } = DateTime.Now;
    public int? ProdutoId { get; set; } // A referência ao produto pode ser nula
    public Produto Produto { get; set; } // Propriedade de navegação
}

}
