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
                ProdutoId = p.Id,
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
        public async Task<IActionResult> Criar(CriarProdutoVM produtoVM)
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
                    QuantidadeDeEstoque = produtoVM.Quantidade
                };

                var historicoEstoque = new HistoricoEstoque
                {
                    Quantidade = produtoVM.Quantidade,
                    DataChegada = DateTime.Now
                };

                produto.HistoricoEstoques.Add(historicoEstoque);

                _db.Produtos.Add(produto);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            await CarregarViewDataCategorias();

            return View(produtoVM);
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            var produto = await _db.Produtos
         .FirstOrDefaultAsync(p => p.Id == Id);

            var viewModel = new EditarProdutoVM
            {
                ProdutoId = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Marca = produto.Marca,
                ImagemUrl = produto.ImagemUrl,
                CategoriaId = produto.CategoriaId
            };

            await CarregarViewDataCategorias();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarProdutoVM viewModel)
        {
            if (ModelState.IsValid)
            {
                // Busca o produto pelo Id
                var produto = await _db.Produtos
                    .FirstOrDefaultAsync(p => p.Id == viewModel.ProdutoId);


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
