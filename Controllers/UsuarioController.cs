using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using App.ViewModels;
using Octavados.Data;
using App.Models;

namespace App.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Contexto _db;

        public UsuarioController(Contexto db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _db.Usuarios.ToListAsync();

            var usuariosVM = usuarios.Select(usuario => new UsuarioVM
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Ativo = usuario.Ativo
            }).ToList();

            return View(usuariosVM);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(UsuarioVM viewmodel)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Id = viewmodel.Id,
                    Email = viewmodel.Email,
                    Senha = viewmodel.Senha,
                    Ativo = viewmodel.Ativo,
                };
                _db.Usuarios.Add(usuario);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            var viewModel = new UsuarioVM
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Ativo = usuario.Ativo
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, UsuarioVM viewmodel)
        {

            var usuario = await _db.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);

            usuario.Email = viewmodel.Email;

            if (ModelState.IsValid)
            {
                if (viewmodel.Senha != viewmodel.ConfirmarSenha)
                {
                    ModelState.AddModelError("ConfirmarSenha", "As senhas não coincidem.");
                }
                else
                {
                    if (!string.IsNullOrEmpty(viewmodel.Senha))
                    {
                        usuario.Senha = viewmodel.Senha;
                    }

                    usuario.Ativo = viewmodel.Ativo;

                    _db.Update(usuario);
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(viewmodel);
        }


    }
}
