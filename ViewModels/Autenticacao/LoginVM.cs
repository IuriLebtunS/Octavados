using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(10, ErrorMessage = "A Senha deve ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string Senha { get; set; } = string.Empty;
    }
}