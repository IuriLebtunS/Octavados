using System.ComponentModel.DataAnnotations;

namespace Octavados.ViewModels
{
    public class CriarEstoqueVM
    {
        
        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        public string NomeProduto { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataChegada { get; set; } = DateTime.Now;
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public int Quantidade { get; set; }
    }
}
