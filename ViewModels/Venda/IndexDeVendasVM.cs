namespace Octavados.ViewModels
{
    public class IndexVendasVM
    {
        public int Id { get; set; } 
        public DateTime DataVenda { get; set; } 
        public decimal ValorDoFrete { get; set; } 
        public decimal Total { get; set; } 
        public int QuantidadeDeItens { get; set; } 
        public string NomeProduto { get; set; } 
    }
}
