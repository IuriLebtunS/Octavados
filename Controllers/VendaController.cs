using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Octavados.ViewModels;
using Octavados.Models;
using Octavados.Data;
using X.PagedList.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;


namespace Octavados.Controllers;

public class VendaController : Controller
{
    private readonly Contexto _db;
    public VendaController(Contexto db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(string nomeCliente, string nomeProduto, decimal? totalVenda, DateTime? dataCompra, int page = 1)
    {
        var vendasQuery = _db.Vendas
            .Include(v => v.Cliente)
            .Include(v => v.ProdutosVenda)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(nomeCliente))
            vendasQuery = vendasQuery.Where(v => v.Cliente.Nome.Contains(nomeCliente));

        if (!string.IsNullOrEmpty(nomeProduto))
            vendasQuery = vendasQuery.Where(v => v.ProdutosVenda.Any(pv => pv.Produto.Nome.Contains(nomeProduto)));

        if (totalVenda.HasValue)
            vendasQuery = vendasQuery.Where(v => v.Total == totalVenda.Value);

        if (dataCompra.HasValue)
            vendasQuery = vendasQuery.Where(v => v.DataVenda.Date == dataCompra.Value.Date);

        var vendasList = await vendasQuery
            .OrderByDescending(v => v.DataVenda)
            .Select(v => new IndexVendasVM
            {
                Id = v.Id,
                DataVenda = v.DataVenda,
                ValorDoFrete = v.ValorDoFrete,
                Total = v.Total,
                QuantidadeDeItens = v.ProdutosVenda.Sum(pv => pv.Quantidade),
                NomeProduto = string.Join(", ", v.ProdutosVenda.Select(pv => pv.Produto.Nome))
            }).ToListAsync();

        var vendasPaged = vendasList.ToPagedList(page, 10);

        var viewModel = new VendasIndexVM
        {
            Filtros = new FiltrosVendasVM
            {
                NomeCliente = nomeCliente,
                NomeProduto = nomeProduto,
                TotalVenda = totalVenda,
                DataCompra = dataCompra
            },
            Vendas = vendasPaged
        };

        return View(viewModel);
    }
    public async Task CarregarViewDataVendas()
    {
        var clientes = await _db.Clientes
                .AsNoTracking()
                .Select(c => new { c.Id, c.Nome })
                .ToListAsync();
        ViewData["Clientes"] = new SelectList(clientes, "Id", "Nome");

        var produtos = await _db.Produtos
                          .AsNoTracking()
                          .Select(p => new { p.Id, p.Nome })
                          .ToListAsync();

        ViewData["Produtos"] = new SelectList(produtos, "Id", "Nome");

    }

    public async Task<IActionResult> Criar()
    {
        await CarregarViewDataVendas();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarVendaViewModel model)
    {
        if (ModelState.IsValid)
        {
            var venda = new Venda
            {
                ClienteId = model.ClienteId,
                DataVenda = model.DataVenda,
                ValorDoFrete = model.ValorDoFrete,
            };

            _db.Vendas.Add(venda);
            await _db.SaveChangesAsync();

            return RedirectToAction("AdicionarProdutos", new { id = venda.Id });
        }

        await CarregarViewDataVendas();
        return View(model);
    }

    public async Task<IActionResult> AdicionarProdutos(int id)
    {
        var venda = await _db.Vendas
            .Include(v => v.ProdutosVenda)
            .FirstOrDefaultAsync(v => v.Id == id);

        await CarregarViewDataVendas();

        return View(venda);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarProdutos(int vendaId, ProdutoVendaViewModel model)
    {
        if (ModelState.IsValid)
        {
            var produto = await _db.Produtos.FindAsync(model.ProdutoId);

            if (produto.QuantidadeDeEstoque < model.Quantidade)
            {
                ModelState.AddModelError("", "Estoque insuficiente para a quantidade solicitada.");
                return RedirectToAction("AdicionarProdutos", new { id = vendaId });
            }

            var venda = await _db.Vendas
                .Include(v => v.ProdutosVenda)
                .FirstOrDefaultAsync(v => v.Id == vendaId);

            var produtoVenda = new ProdutoVenda
            {
                ProdutoId = model.ProdutoId,
                Quantidade = model.Quantidade,
                PrecoUnitario = model.PrecoUnitario,
                Desconto = model.Desconto
            };

            venda.ProdutosVenda.Add(produtoVenda);


            produto.QuantidadeDeEstoque -= model.Quantidade;

            _db.ProdutoVendas.Add(produtoVenda);

            await _db.SaveChangesAsync();

            return RedirectToAction("AdicionarProdutos", new { id = vendaId });
        }

        await CarregarViewDataVendas();
        return View(model);
    }

}

