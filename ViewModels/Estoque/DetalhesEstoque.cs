using System;
using System.ComponentModel.DataAnnotations;

namespace Octavados.ViewModels
{
    public class DetalhesEstoqueVM
    {
        public int ProdutoId { get; set; }

        [Display(Name = "Quantidade Adicionada na Última Atualização")]
        public int? QuantidadeAdicionada { get; set; }

        [Display(Name = "Data da Última Atualização")]
        [DataType(DataType.Date)]
        public DateTime? DataAtualizacao { get; set; }

    }
}
