using Microsoft.EntityFrameworkCore;
using projetoX.Models;
using BCrypt.Net;

namespace projetoX.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Usuario> Usuarios { get; set; } // Nova entidade
public DbSet<RefreshToken> RefreshTokens { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>()
                .HasOne(e => e.Empresa)
                .WithMany(emp => emp.Enderecos)
                .HasForeignKey(e => e.EmpresaId);

            // Seed do administrador
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nome = "Administrador",
                    Email = "admin@projetox.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123"), // Senha padrão criptografada
                    IsAdmin = true
                }
            );
        }
    }
}