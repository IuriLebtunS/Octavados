using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Octavados.ViewModels;
using Octavados.Models;
using Octavados.Data;

namespace Octavados.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly Contexto _db;

        public ProdutoController(Contexto context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _db.Produtos
                .Include(p => p.Categoria)
                .ToListAsync();

            var produtosNovo = produtos.Select(p => new IndexDeProdutosVM
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Marca = p.Marca,
                QuantidadeEmEstoque = p.QuantidadeEmEstoque,
                Imagem = p.ImagemUrl,
                CategoriaNome = p.Categoria.Nome
            }).ToList();

            return View(produtosNovo);
        }

        public async Task<IActionResult> Criar()
        {
            await CarregarViewDataCategorias();
            return View();
        }


        public async Task CarregarViewDataCategorias()
        {
            var categorias = await _db.Categorias
                .AsNoTracking()
                .Select(c => new { c.Id, c.Nome })
                .ToListAsync();
            ViewData["Categorias"] = new SelectList(categorias, "Id", "Nome");
        }


        [HttpPost]
        public async Task<IActionResult> Criar(CriarProdutoVM produtoVM)
        {
            if (ModelState.IsValid)
            {
                var produto = new Produto
                {
                    Nome = produtoVM.Nome,
                    Preco = produtoVM.Preco,
                    Marca = produtoVM.Marca,
                    QuantidadeEmEstoque = produtoVM.QuantidadeEmEstoque,
                    ImagemUrl = produtoVM.ImagemUrl,
                    CategoriaId = produtoVM.CategoriaId
                };

                _db.Produtos.Add(produto);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            await CarregarViewDataCategorias();

            return View(produtoVM);
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var produto = await _db.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            var viewModel = new EditarProdutoVM
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Marca = produto.Marca,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque,
                ImagemUrl = produto.ImagemUrl,
                CategoriaId = produto.CategoriaId,

            };

            await CarregarViewDataCategorias();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarProdutoVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var produto = await _db.Produtos.FindAsync(viewModel.Id);

                if (produto == null)
                {
                    return NotFound();
                }

                produto.Nome = viewModel.Nome;
                produto.Preco = viewModel.Preco;
                produto.Marca = viewModel.Marca;
                produto.QuantidadeEmEstoque = viewModel.QuantidadeEmEstoque;
                produto.ImagemUrl = viewModel.ImagemUrl;
                produto.CategoriaId = viewModel.CategoriaId;

                _db.Entry(produto).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            await CarregarViewDataCategorias();


            ModelState.AddModelError("CategoriaId", "Selecione uma categoria.");

            return View(viewModel);
        }

    }
}
