
namespace Octavados.Models
{
    public class Estoque
    {
        public Estoque() { }
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public int Quantidade { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime DataChegada { get; set; }
    }
}
