using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Octavados.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Octavados.ViewModels;
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


        public async Task CarregarViewDataProduto()
        {

            var produtos = await _db.Produtos
              .AsNoTracking()
              .Select(p => new { p.Id, p.Nome })
              .ToListAsync();
            ViewData["Produtos"] = new SelectList(produtos, "Id", "Nome");

        }

        public async Task<ActionResult> ListarEstoqueNovo()
        {
            var estoques = await _db.Estoques
               .Include(e => e.Produto)
               .ToListAsync();

            var viewModel = estoques.Select(e => new ListarEstoqueNovoVM
            {
                NomeProduto = e.Produto.Nome,
                Quantidade = e.Quantidade,
                DataChegada = e.DataChegada
            }).ToList();

            return View(viewModel);
        }

        public async Task<ActionResult> AdicionarEstoque()
        {
            await CarregarViewDataProduto();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarEstoque(AdicionarEstoqueVM model)
        {
            if (ModelState.IsValid)
            {
                var produto = await _db.Produtos.FindAsync(model.ProdutoId);

                produto?.AtualizarEstoque(model.Quantidade);

                var estoque = new Estoque
                {
                    ProdutoId = model.ProdutoId,
                    NomeProduto = model.NomeProduto,
                    Quantidade = model.Quantidade,
                    DataChegada = model.DataChegada
                };

                _db.Estoques.Add(estoque);
                await _db.SaveChangesAsync();

                return RedirectToAction("ListarEstoqueNovo");
            }

            await CarregarViewDataProduto();
            return View(model);
        }
    }
}
