// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using LojaManoel.Data.Entities; // NOVO USING AQUI

namespace LojaManoel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Mapeie para as novas classes de ENTIDADE
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ProdutoEntity> ProdutosOriginaisDb { get; set; } // Mude o nome do DbSet para ProdutoEntity
        public DbSet<CaixaEntity> CaixasUtilizadasDb { get; set; } // Mude o nome do DbSet para CaixaEntity
        public DbSet<ProdutoEmbaladoEntity> ProdutosEmbaladosNaCaixaDb { get; set; } // Mude o nome do DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de relacionamento usando as novas entidades
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.ProdutosOriginais) // ProdutosOriginais é a lista em Pedido (entidade DB)
                .WithOne(po => po.Pedido)
                .HasForeignKey(po => po.PedidoId);

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Caixas)
                .WithOne(c => c.Pedido)
                .HasForeignKey(c => c.PedidoId);

            modelBuilder.Entity<CaixaEntity>() // Use CaixaEntity aqui
                .HasMany(c => c.ProdutosEmbalados)
                .WithOne(pe => pe.Caixa)
                .HasForeignKey(pe => pe.CaixaId);

            // Mapeamento das tabelas (já dentro das classes com [Table("NomeDaTabela")])
            // modelBuilder.Entity<Pedido>().ToTable("Pedidos"); // Já feito via [Table]
            // ...
        }
    }
}