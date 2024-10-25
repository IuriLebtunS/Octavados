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
                Imagem = p.ImagemUrl,
                CategoriaNome = p.Categoria.Nome
            }).ToList();

            return View(produtosNovo);
        }



        public async Task CarregarViewDataCategorias()
        {
            var categorias = await _db.Categorias
                .AsNoTracking()
                .Select(c => new { c.Id, c.Nome })
                .ToListAsync();
            ViewData["Categorias"] = new SelectList(categorias, "Id", "Nome");
        }

        public async Task<IActionResult> Criar()
        {
            await CarregarViewDataCategorias();
            return View(new CriarProdutoVM());
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarProdutoVM produtoVM, int estoqueId)
        {
            if (ModelState.IsValid)
            {

                var produto = new Produto
                {
                    Nome = produtoVM.Nome,
                    Preco = produtoVM.Preco,
                    Marca = produtoVM.Marca,
                    ImagemUrl = produtoVM.ImagemUrl,
                    CategoriaId = produtoVM.CategoriaId,
                    EstoqueId = produtoVM.EstoqueId
                };

                _db.Produtos.Add(produto);
                await _db.SaveChangesAsync();

                var estoque = await _db.Estoques.FindAsync(estoqueId);
                if (estoque != null)
                {
                    estoque.ProdutoId = produto.Id;
                    await _db.SaveChangesAsync();
                }
            

                return RedirectToAction("Index");
            }

            await CarregarViewDataCategorias();

            return View(produtoVM);
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var produto = await _db.Produtos
                .Include(p => p.Estoque)
                .FirstOrDefaultAsync(p => p.Id == id);

            var viewModel = new EditarProdutoVM
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Marca = produto.Marca,
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
                var produto = await _db.Produtos
                .Include(p => p.Estoque)
                .FirstOrDefaultAsync(p => p.Id == viewModel.Id);

                produto.Nome = viewModel.Nome;
                produto.Preco = viewModel.Preco;
                produto.Marca = viewModel.Marca;
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
