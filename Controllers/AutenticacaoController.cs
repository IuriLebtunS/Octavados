using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Octavados.Data;

namespace App.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly Contexto _db;
        private readonly TiaIdentity.Autenticador tiaIdentity;

        public AutenticacaoController(Contexto db, TiaIdentity.Autenticador tiaIdentity)
        {
            this._db = db;
            this.tiaIdentity = tiaIdentity;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewmodel)
        {
              if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            var usuario = await _db.Usuarios.FirstOrDefaultAsync(a => a.Email == viewmodel.Email);

            if (usuario == null || usuario.Senha != viewmodel.Senha)
            {
                ModelState.AddModelError("", "Credenciais inválidas!");
                return View(viewmodel);
            }

            await tiaIdentity.LoginAsync(usuario.Email, usuario.Email, false, "Usuário");

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await tiaIdentity.LogoutAsync();
            return View(nameof(Login));
        }
    }
}