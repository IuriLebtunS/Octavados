using Microsoft.EntityFrameworkCore;
using Octavados.Models;

namespace Octavados.Contexto;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options): base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Detalhe> Detalhes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Venda> Vendas { get; set; }
}
