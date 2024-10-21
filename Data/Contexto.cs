using Microsoft.EntityFrameworkCore;
using Octavados.Models;

namespace Octavados.Data;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Detalhe> Detalhes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<Estoque> Estoques { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurando a relação um-para-um entre Produto e Estoque
        modelBuilder.Entity<Produto>()
            .HasOne(p => p.Estoque)
            .WithOne(e => e.Produto)
            .HasForeignKey<Estoque>(e => e.ProdutoId); // Supondo que ProdutoId é a chave estrangeira em Estoque

        // Outras configurações de modelo podem ser adicionadas aqui
    }

}
