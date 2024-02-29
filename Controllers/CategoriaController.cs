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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _db.Categorias.Add(categoria);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var categoria = await _db.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _db.Categorias.Update(categoria);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(categoria);
        }
    }
}
