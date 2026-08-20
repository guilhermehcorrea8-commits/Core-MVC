using CadastroAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroAlunos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aluno>()
                .Property(a => a.Nota)
                .HasPrecision(4, 2);
        }
    }
}