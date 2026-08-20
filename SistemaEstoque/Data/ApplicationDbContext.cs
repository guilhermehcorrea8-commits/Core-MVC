using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Models;

namespace SistemaEstoque.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovimentacaoEstoque>()
                .HasOne(m => m.Produto)
                .WithMany(p => p.Movimentacoes)
                .HasForeignKey(m => m.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}