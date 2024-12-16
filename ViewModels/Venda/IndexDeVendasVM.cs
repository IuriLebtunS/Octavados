using X.PagedList;

namespace Octavados.ViewModels;

    public class IndexVendasVM
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorDoFrete { get; set; }
        public decimal? Total { get; set; }
        public int QuantidadeDeItens { get; set; }
        public string NomeProduto { get; set; }
    }

    
    public class VendasIndexVM
    {
        public FiltrosVendasVM Filtros { get; set; } = new FiltrosVendasVM();
        public IPagedList<IndexVendasVM> Vendas { get; set; }
    }

    public class FiltrosVendasVM
    {
        public string NomeCliente { get; set; }
        public string NomeProduto { get; set; }
        public decimal? TotalVenda { get; set; }
        public DateTime? DataCompra { get; set; }
    }

