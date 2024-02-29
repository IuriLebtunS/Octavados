using Microsoft.AspNetCore.Mvc.Rendering;

namespace Octavados.ViewModels
{
    public class EditarProdutoVM
    {
        public string Nome { get; set; }
        public string CategoriaNome { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public string? Imagem { get; set; }

        public IFormFile? ImagemUpload { get; set; }

        public int CategoriaId { get; set; }
        public List<SelectListItem> Categorias { get; set; }

    }
}