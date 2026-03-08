using Microsoft.EntityFrameworkCore;
using DomesticoDireitos.Api.Models;

namespace DomesticoDireitos.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Tabelas do Tema Direitos Domésticos
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Subitem> Subitens { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<DiagnosticoItem> DiagnosticosItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Chave Composta (Muitos-para-Muitos) - Crucial para SQLite e SQL Server
            modelBuilder.Entity<DiagnosticoItem>()
                .HasKey(di => new { di.DiagnosticoId, di.SubitemId });

            // 2. Configuração Explícita de Relacionamentos (Evita erros de mapeamento)
            modelBuilder.Entity<DiagnosticoItem>()
                .HasOne(di => di.Diagnostico)
                .WithMany(d => d.ItensSelecionados)
                .HasForeignKey(di => di.DiagnosticoId);

            modelBuilder.Entity<DiagnosticoItem>()
                .HasOne(di => di.Subitem)
                .WithMany()
                .HasForeignKey(di => di.SubitemId);

            // 3. Relacionamento Categoria -> Subitens
            modelBuilder.Entity<Subitem>()
                .HasOne(s => s.Categoria)
                .WithMany(c => c.Subitens)
                .HasForeignKey(s => s.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict); // Segurança: Não apaga a categoria se houver subitens

            // 4. Mapeamento de Nomes de Tabela (Garante consistência entre bancos)
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Subitem>().ToTable("Subitens");
            modelBuilder.Entity<Diagnostico>().ToTable("Diagnosticos");
            modelBuilder.Entity<DiagnosticoItem>().ToTable("DiagnosticosItens");
        }
    }
}