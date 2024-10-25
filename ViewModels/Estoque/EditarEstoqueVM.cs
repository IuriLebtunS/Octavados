using System.ComponentModel.DataAnnotations;

namespace Octavados.ViewModels
{
    public class EditarEstoqueVM
    {
        public int ProdutoId { get; set; }

        [Display(Name = "Produto")]
        public string NomeProduto { get; set; }

        [Display(Name = "Estoque")]
        [Required(ErrorMessage = "Adicione um novo Estoque.")]
        public int Quantidade { get; set; }

        [Display(Name = "Estoque Adicionado")]
        public int? QuantidadeAdicionada { get; set; }

        [Display(Name = "Atualizacao Do Estoque")]
        [DataType(DataType.Date)]
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
