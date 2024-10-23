using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Octavados.Data;
using Octavados.Migrations;
using Octavados.Models;
using Octavados.Models.ViewModels;
using Octavados.ViewModels;

namespace Octavados.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly Contexto _db;

        public EstoqueController(Contexto context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var estoque = await _db.Estoques
                .Include(e => e.Produto) 
                .OrderBy(c => c.DataChegada) 
                .Select(c => new IndexEstoqueVM
                {
                    Id = c.Id,
                    NomeProduto = c.NomeProduto,
                    DataChegada = c.DataChegada,
                    Quantidade = c.Quantidade
                })
                .ToListAsync();

            return View(estoque);
        }

        public IActionResult Criar()
        {
            var criarEstoqueVM = new CriarEstoqueVM();
            return View(criarEstoqueVM);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarEstoqueVM criarEstoqueVM)
        {
            if (ModelState.IsValid)
            {
                var estoque = new Estoque
                {
                    ProdutoId = criarEstoqueVM.ProdutoId,
                    NomeProduto = criarEstoqueVM.NomeProduto,
                    Quantidade = criarEstoqueVM.Quantidade,
                    DataChegada = criarEstoqueVM.DataChegada
                };

                _db.Estoques.Add(estoque);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(criarEstoqueVM);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var estoque = await _db.Estoques.FindAsync(id);

            var editarEstoqueVM = new EditarEstoqueVM
            {
                ProdutoId = estoque.ProdutoId,
                NomeProduto = estoque.NomeProduto,
                DataChegada = estoque.DataChegada,
                Quantidade = estoque.Quantidade
            };

            return View(editarEstoqueVM);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, EditarEstoqueVM editarEstoqueVM)
        {

            if (ModelState.IsValid)
            {
                var estoque = await _db.Estoques.FindAsync(id);

                estoque.ProdutoId = editarEstoqueVM.ProdutoId;
                estoque.NomeProduto = editarEstoqueVM.NomeProduto;
                estoque.DataChegada = editarEstoqueVM.DataChegada;
                estoque.Quantidade = editarEstoqueVM.Quantidade;

                _db.Estoques.Update(estoque);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(editarEstoqueVM);
        }


    }
}
