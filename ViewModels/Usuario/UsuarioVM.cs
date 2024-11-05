namespace App.ViewModels
{
    public class UsuarioVM
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;
        public string ConfirmarSenha { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
