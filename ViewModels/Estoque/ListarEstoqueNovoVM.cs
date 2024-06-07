namespace Octavados.Models.ViewModels
{
    public class ListarEstoqueNovoVM
    {
        public int Id { get; set; }
        public string CategoriaNome { get; set; }
        public string NomeProduto { get; set; }
        public string Marca { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataChegada { get; set; }
    }
}
