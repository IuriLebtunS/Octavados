
namespace Octavados.Models
{
    public class Estoque
    {
        public Estoque() { }
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataChegada { get; set; }
    }
}
