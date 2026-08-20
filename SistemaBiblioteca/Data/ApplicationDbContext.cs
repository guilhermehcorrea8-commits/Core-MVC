using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Categoria> Categorias { get; set; }
    }
}