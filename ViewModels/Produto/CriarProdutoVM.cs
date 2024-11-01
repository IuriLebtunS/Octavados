using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Octavados.ViewModels
{
    public class CriarProdutoVM
    { 
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "URL da Imagem", Prompt = "Insira o URL da imagem.")]
        [DataType(DataType.ImageUrl)]
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
        [Display(Name = "Quantidade em Estoque", Prompt = "Informe a quantidade inicial de estoque.")]
        public int Quantidade { get; set; }
    }
}
