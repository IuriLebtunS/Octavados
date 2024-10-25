
namespace Octavados.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataChegada { get; set; } = DateTime.Now;
        public int? ProdutoId { get; set; } 
        public Produto Produto { get; set; }
        public int QuantidadeAdicionada { get; set; }  
        public DateTime DataAtualizacao { get; set; } 
    }

}
