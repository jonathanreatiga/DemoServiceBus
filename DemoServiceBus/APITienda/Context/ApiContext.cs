using ApiTienda.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiTienda.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext() { }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Producto> Producto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entry => 
            {
                entry.ToTable("Productos");
                entry.HasKey(e => e.IdProducto);
            });

            modelBuilder.Entity<Pedido>(entry =>
            {
                entry.ToTable("Pedidos");
                entry.HasKey(e => e.IdPedido);
            });
        }
    }
}
