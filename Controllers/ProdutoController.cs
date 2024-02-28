using Microsoft.EntityFrameworkCore;
using Octavados.Models;
using Octavados.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index()
        {
            var produtos = _db.Produtos
                .Include(p => p.Categoria)
                .ToList();

            var produtosNovo = produtos.Select(p => new IndexDeProdutosVM
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Marca = p.Marca,
                QuantidadeEmEstoque = p.QuantidadeEmEstoque,
                Imagem = p.Imagem,
                CategoriaNome = p.Categoria?.Nome
            }).ToList();

            return View(produtosNovo);
        }


        public IActionResult Criar()
        {
            var categorias = _db.Categorias
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                }).ToList();

            var produtoVM = new CriarProdutoVM
            {
                Categorias = categorias
            };

            return View(produtoVM);
        }

        [HttpPost]
        public IActionResult Criar(CriarProdutoVM produtoVM)
        {
            if (ModelState.IsValid)
            {
                // Mapear dados da ViewModel para a entidade Produto
                var produto = new Produto
                {
                    Nome = produtoVM.Nome,
                    Preco = produtoVM.Preco,
                    Marca = produtoVM.Marca,
                    QuantidadeEmEstoque = produtoVM.QuantidadeEmEstoque,
                    Imagem = produtoVM.ImagemUpload?.FileName,
                    CategoriaId = produtoVM.CategoriaId
                };

                _db.Produtos.Add(produto);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            produtoVM.Categorias = _db.Categorias
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                }).ToList();

            return View(produtoVM);
        }


        public IActionResult Editar(int id)
        {
            var produto = _db.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = new SelectList(_db.Categorias.ToList(), "Id", "Nome", produto.CategoriaId);

            return View(produto);
        }

        [HttpPost]
        public IActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(produto).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Categorias = new SelectList(_db.Categorias.ToList(), "Id", "Nome", produto.CategoriaId);

            return View(produto);
        }

        public IActionResult Detalhes(int id)
        {
            var produto = _db.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
    }
}
