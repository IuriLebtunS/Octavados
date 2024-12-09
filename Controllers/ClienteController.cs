using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Octavados.Models;
using Octavados.ViewModels;
using Octavados.Data;
using Microsoft.AspNetCore.Authorization;
using X.PagedList.Extensions;

namespace Octavados.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly Contexto _db;

        public ClienteController(Contexto context)
        {
            _db = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(string nomeCliente, string cpf, string email, int page = 1)
        {
            var clientesQuery = _db.Clientes
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(nomeCliente))
                clientesQuery = clientesQuery.Where(c => c.Nome.Contains(nomeCliente));

            if (!string.IsNullOrEmpty(cpf))
                clientesQuery = clientesQuery.Where(c => c.CPF.Contains(cpf));

            if (!string.IsNullOrEmpty(email))
                clientesQuery = clientesQuery.Where(c => c.Email.Contains(email));

            var listaDeClientes = await clientesQuery
                .OrderBy(c => c.Nome)
                .Select(c => new IndexDeClientesVM
                {
                    ClienteId = c.Id,
                    Nome = c.Nome,
                    CPF = c.CPF,
                    Email = c.Email,
                    Telefone = c.Telefone
                }).ToListAsync();

            var clientesPaged = listaDeClientes.ToPagedList(page, 10);
            return View(clientesPaged);
        }

        public IActionResult Criar()
        {
            return View(new CriarClienteVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CriarClienteVM clienteVM)
        {
            if (ModelState.IsValid)
            {
                var cliente = new Cliente
                {
                    Nome = clienteVM.Nome,
                    CPF = clienteVM.CPF,
                    Email = clienteVM.Email,
                    Telefone = clienteVM.Telefone,
                    Endereco = clienteVM.Endereco
                };

                _db.Clientes.Add(cliente);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(clienteVM);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var cliente = await _db.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            var clienteVM = new EditarClienteVM
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                Endereco = cliente.Endereco
            };

            return View(clienteVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(EditarClienteVM clienteVM)
        {
            if (ModelState.IsValid)
            {
                var cliente = await _db.Clientes.FindAsync(clienteVM.Id);
                if (cliente == null)
                    return NotFound();

                cliente.Nome = clienteVM.Nome;
                cliente.CPF = clienteVM.CPF;
                cliente.Email = clienteVM.Email;
                cliente.Telefone = clienteVM.Telefone;
                cliente.Endereco = clienteVM.Endereco;

                _db.Entry(cliente).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(clienteVM);
        }



    }
}
