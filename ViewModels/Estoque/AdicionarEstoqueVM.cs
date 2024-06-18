using System.ComponentModel.DataAnnotations;

namespace Octavados.ViewModels
{
    public class AdicionarEstoqueVM
    {
        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O campo NomeProduto é obrigatório.")]
        public string NomeProduto { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public int Quantidade { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataChegada { get; set; } = DateTime.Today;
    }
}
