using MarketPlace.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Data
{
    public class MarketPlaceDbContext : DbContext
    {
        public MarketPlaceDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<ItemDoPedido> ItemDoPedido { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Produto> Produto { get; set; }

        // DENTRO DO SEU ARQUIVO MarketPlaceDbContext.cs

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pessoa>()
                .HasOne(pessoa => pessoa.Usuario)
                .WithOne(usuario => usuario.Pessoa)
                .HasForeignKey<Usuario>(usuario => usuario.PessoaId);
                
             base.OnModelCreating(modelBuilder); 
        } 
    }
}