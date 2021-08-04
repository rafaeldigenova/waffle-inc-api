using Microsoft.EntityFrameworkCore;
using Waffle.Inc.Domain;

namespace Waffle.Inc.Repositories
{
    public class WaffleIncDbContext : DbContext
    {
        public WaffleIncDbContext(DbContextOptions<WaffleIncDbContext> options) : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>().ToTable("Funcionarios")
                .HasIndex(p => p.NumeroChapa)
                .IsUnique(true);
            modelBuilder.Entity<Telefone>().ToTable("Telefones");
        }
    }
}
