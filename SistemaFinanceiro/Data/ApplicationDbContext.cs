using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Models;

namespace SistemaFinanceiro.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Receita> Receitas { get; set; }
    public DbSet<Despesa> Despesas { get; set; }
    public DbSet<Lancamento> Lancamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Receita>()
            .Property(r => r.Valor)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Despesa>()
            .Property(d => d.Valor)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Lancamento>()
            .Property(l => l.Valor)
            .HasPrecision(18, 2);
    }
}