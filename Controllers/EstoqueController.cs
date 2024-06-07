using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Octavados.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Octavados.Models;
using Octavados.Data;

namespace Octavados.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly Contexto _db;

        public EstoqueController(Contexto context)
        {
            _db = context;
        }


        public async Task CarregarViewDataCategorias()
        {
            var categorias = await _db.Categorias
                .AsNoTracking()
                .Select(c => new { c.Id, c.Nome })
                .ToListAsync();
            ViewData["Categorias"] = new SelectList(categorias, "Id", "Nome");

            var produtos = await _db.Produtos
              .AsNoTracking()
              .Select(p => new { p.Id, p.Nome })
              .ToListAsync();
            ViewData["Produtos"] = new SelectList(produtos, "Id", "Nome");

        }

        public async Task<ActionResult> ListarEstoqueNovo()
        {
            var estoques = _db.Estoques.Include(e => e.Categoria).ToList();

            var viewModel = estoques.Select(e => new ListarEstoqueNovoVM
            {
                Categoria = e.Categoria.Nome,
                NomeProduto = e.Nome,
                Quantidade = e.Quantidade,
                DataChegada = e.DataChegada
            }).ToList();

            return View(viewModel);
        }

        public async Task<ActionResult> AdicionarEstoque()
        {
            await CarregarViewDataCategorias();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdicionarEstoque(Estoque model)
        {
            if (ModelState.IsValid)
            {
                _db.Estoques.Add(model);
                await _db.SaveChangesAsync();
                return RedirectToAction("ListarEstoqueNovo");
            }

           await CarregarViewDataCategorias();
            return View(model);
        }
    }
}
