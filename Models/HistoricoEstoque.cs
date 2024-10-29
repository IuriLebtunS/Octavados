
namespace Octavados.Models
{
    public class HistoricoEstoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataChegada { get; set; } = DateTime.Now;
        public Produto Produto { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}