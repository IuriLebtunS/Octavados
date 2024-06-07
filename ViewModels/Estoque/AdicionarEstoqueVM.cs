using System;

namespace Octavados.ViewModels
{
    public class AdicionarEstoqueVM
    {
        public int CategoriaId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataChegada { get; set; }
    }
}