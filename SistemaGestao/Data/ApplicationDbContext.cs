using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaGestao.Models;

namespace SistemaGestao.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<MetaFinanceira> MetasFinanceiras { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Receita>()
                .Property(r => r.Valor)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Despesa>()
                .Property(d => d.Valor)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Conta>()
                .Property(c => c.Saldo)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MetaFinanceira>()
                .Property(m => m.ValorObjetivo)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MetaFinanceira>()
                .Property(m => m.ValorAtual)
                .HasPrecision(18, 2);

            // Relacionamento Movimentacao -> Conta
            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Conta)
                .WithMany(c => c.Movimentacoes)
                .HasForeignKey(m => m.ContaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Receita -> Categoria
            modelBuilder.Entity<Receita>()
                .HasOne(r => r.Categoria)
                .WithMany(c => c.Receitas)
                .HasForeignKey(r => r.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Despesa -> Categoria
            modelBuilder.Entity<Despesa>()
                .HasOne(d => d.Categoria)
                .WithMany(c => c.Despesas)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}