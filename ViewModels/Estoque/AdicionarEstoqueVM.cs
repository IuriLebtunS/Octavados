using Microsoft.AspNetCore.Mvc.Rendering;

namespace Octavados.ViewModels
{
    public class AdicionarEstoqueVM
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public int Quantidade { get; set; }
        public int CategoriaId { get; set; }

    }
}
