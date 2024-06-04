using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Octavados.Data;
using Octavados.Models;
using Octavados.ViewModels;

namespace Octavados.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly Contexto _db;

        public CategoriaController(Contexto context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _db.Categorias
                .Select(c => new IndexCategoriaVM
                {
                    Id = c.Id,
                    Nome = c.Nome
                })
                .ToListAsync();

            return View(categorias);
        }
        public IActionResult Criar()
        {
            var criarCategoriaVM = new CriarCategoriaVM();
            return View(criarCategoriaVM);
        }


        [HttpPost]
        public async Task<IActionResult> Criar(CriarCategoriaVM criarCategoriaVM)
        {
            if (ModelState.IsValid)
            {
                var categoria = new Categoria
                {
                    Nome = criarCategoriaVM.Nome
                };

                _db.Categorias.Add(categoria);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(criarCategoriaVM);
        }


        public async Task<IActionResult> Editar(int id)
        {
            var categoria = await _db.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            var editarCategoriaVM = new EditarCategoriaVM
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Ativo = categoria.Ativo
            };

            return View(editarCategoriaVM);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarCategoriaVM editarCategoriaVM)
        {
            if (ModelState.IsValid)
            {
                var categoria = new Categoria
                {
                    Id = editarCategoriaVM.Id,
                    Nome = editarCategoriaVM.Nome,
                    Ativo = editarCategoriaVM.Ativo
                };

                _db.Categorias.Update(categoria);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(editarCategoriaVM);
        }


    }
}
