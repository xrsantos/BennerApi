using LojaBenner.Entities;
using Microsoft.EntityFrameworkCore;

namespace LojaBenner.Contexts;

public class BennerContext : DbContext
{
    public BennerContext(DbContextOptions<BennerContext> o) : base(o) { }

    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Pessoa>()
            .HasIndex(p => p.Cpf).IsUnique();

        b.Entity<Produto>()
            .HasIndex(p => p.Codigo).IsUnique();

        b.Entity<PedidoItem>()
            .HasOne(i => i.Produto)
            .WithMany().OnDelete(DeleteBehavior.Restrict);
        
    }
}
