using ApiFinanciera.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanciera.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext() { }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Producto> Producto { get; set; }
        public DbSet<Ganancia> Ganancia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ganancia>(entry =>
            {
                entry.ToTable("Ganancias");
                entry.HasKey(e => e.IdGanancia);
            });

            modelBuilder.Entity<Producto>(entry =>
            {
                entry.ToTable("Productos");
                entry.HasKey(e => e.IdProducto);
            });
        }
    }
}
