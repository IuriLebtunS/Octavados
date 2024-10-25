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
                    NomeProduto = criarEstoqueVM.NomeProduto,
                    Quantidade = criarEstoqueVM.Quantidade,
                    DataChegada = DateTime.Now
                };


                _db.Estoques.Add(estoque);
                await _db.SaveChangesAsync();

                return RedirectToAction("Criar", "Produto", new { estoqueId = estoque.Id });
            }

            return View(criarEstoqueVM);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var estoque = await _db.Estoques.FindAsync(id);

            var editarEstoqueVM = new EditarEstoqueVM
            {
                NomeProduto = estoque.NomeProduto,
                Quantidade = estoque.Quantidade,
                QuantidadeAdicionada = estoque.QuantidadeAdicionada,
                DataAtualizacao = estoque.DataAtualizacao
            };
            return View(editarEstoqueVM);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, EditarEstoqueVM editarEstoqueVM)
        {

            if (ModelState.IsValid)
            {
                var estoque = await _db.Estoques.FindAsync(id);

                estoque.NomeProduto = editarEstoqueVM.NomeProduto;
                estoque.Quantidade += editarEstoqueVM.Quantidade; 
                estoque.QuantidadeAdicionada = editarEstoqueVM.Quantidade; 
                estoque.DataAtualizacao = DateTime.Now;

                _db.Estoques.Update(estoque);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(editarEstoqueVM);
        }


    }
}
