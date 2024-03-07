using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Octavados.Data;
using Octavados.Models;
using Octavados.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                CategoriaNome = p.Categoria?.Nome
            }).ToList();

            return View(produtosNovo);
        }

        public async Task<IActionResult> Criar()
        {
            ViewData["Categorias"] = new SelectList(await _db.Categorias
                .Select(c => new { Value = c.Id.ToString(), Text = c.Nome })
                .ToListAsync(), "Value", "Text");

            return View();
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

            ViewData["Categorias"] = new SelectList(await _db.Categorias
                .Select(c => new { Value = c.Id.ToString(), Text = c.Nome })
                .ToListAsync(), "Value", "Text");

            return View(produtoVM);
        }


      public async Task<IActionResult> Editar(int id)
{
    var produto = await _db.Produtos.FindAsync(id);

    if (produto == null)
    {
        return NotFound();
    }

    var viewModel = new EditarProdutoVM
    {
        Nome = produto.Nome,
        Preco = produto.Preco,
        Marca = produto.Marca,
        QuantidadeEmEstoque = produto.QuantidadeEmEstoque,
        ImagemUrl = produto.ImagemUrl, 
        CategoriaId = produto.CategoriaId
    };

    // Passe a lista de categorias para a ViewData
    ViewData["Categorias"] = new SelectList(_db.Categorias.ToList(), "Id", "Nome", produto.CategoriaId);

    return View(viewModel);
}

        [HttpPost]
        public async Task<IActionResult> Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(produto).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.Categorias = new SelectList(_db.Categorias.ToList(), "Id", "Nome", produto.CategoriaId);

            return View(produto);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _db.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
    }
}
