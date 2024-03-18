using Microsoft.AspNetCore.Mvc.Rendering;

namespace Octavados.ViewModels
{
    public class EditarProdutoVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public string ImagemUrl { get; set; }
        public int CategoriaId { get; set; }

    }
}