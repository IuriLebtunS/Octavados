namespace Octavados.ViewModels
{
    public class IndexDeProdutosVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public string Imagem { get; set; }
        public string CategoriaNome { get; set; }
    }
}
