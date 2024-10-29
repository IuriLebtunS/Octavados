namespace Octavados.ViewModels
{
    public class IndexEstoqueVM
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public DateTime DataChegada { get; set; }
        public int Quantidade { get; set; }
    }

}
