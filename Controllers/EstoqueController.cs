using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Octavados.Data;
using Octavados.Migrations;
using Octavados.Models;
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
                    ProdutoId = c.Id,
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
                    NomeProduto = criarEstoqueVM.NomeProduto,
                    Quantidade = criarEstoqueVM.Quantidade,
                    DataChegada = DateTime.Now
                };


                _db.Estoques.Add(estoque);
                await _db.SaveChangesAsync();

                return RedirectToAction("Criar", "Produto", new { estoqueId = estoque.ProdutoId });
            }

            return View(criarEstoqueVM);
        }

        public async Task<IActionResult> Editar(int Id)
        {
            var estoque = await _db.Estoques.FirstOrDefaultAsync(e => e.Id == Id);

            var editarEstoqueVM = new EditarEstoqueVM
            {
                Id = estoque.Id,
                ProdutoId = estoque.ProdutoId, 
                NomeProduto = estoque.NomeProduto,
                Quantidade = estoque.Quantidade,
                QuantidadeAdicionada = estoque.QuantidadeAdicionada ?? 0,
                DataAtualizacao = estoque.DataAtualizacao ?? DateTime.Now
            };
            return View(editarEstoqueVM);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarEstoqueVM editarEstoqueVM)
        {

            if (ModelState.IsValid)
            {
                var estoque = await _db.Estoques.FirstOrDefaultAsync(e => e.Id == editarEstoqueVM.Id);

                estoque.NomeProduto = editarEstoqueVM.NomeProduto;
                estoque.Quantidade += editarEstoqueVM.Quantidade;
                estoque.DataAtualizacao = DateTime.Now;

                _db.Estoques.Update(estoque);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(editarEstoqueVM);
        }
    }
}
