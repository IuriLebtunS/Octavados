using Microsoft.AspNetCore.Mvc.Rendering;

namespace Octavados.ViewModels
{
    public class CriarProdutoVM
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public string ImagemUrl { get; set; }
        public int CategoriaId { get; set; }
        public int Quantidade { get; set; }
    }
}
