using FunctionFinanciera.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionFinanciera.Context
{
    public class FunctionContext : DbContext
    {
        public FunctionContext(DbContextOptions<FunctionContext> options) : base(options)
        {
        }

        public DbSet<Producto> Producto { get; set; }
        public DbSet<Ganancia> Ganancia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Producto>(entry =>
            {
                entry.ToTable("Productos");
                entry.HasKey(e => e.IdProducto);
            });

            modelBuilder.Entity<Ganancia>(entry =>
            {
                entry.ToTable("Ganancias");
                entry.HasKey(e => e.IdGanancia);
            });
        }
    }
}
