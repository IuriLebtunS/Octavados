namespace Octavados.ViewModels
{
    public class MovimentoEstoqueVM
    {
        public int ProdutoId { get; set; }
        public int Id { get; set; }
        public string Usuario { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataChegada { get; set; }
    }
}