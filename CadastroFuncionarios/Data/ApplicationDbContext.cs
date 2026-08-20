using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroFuncionarios.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroFuncionarios.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}