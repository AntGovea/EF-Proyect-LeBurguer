using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class ConexionDBContext : DbContext
    {
        private const string ConnectionString = "DefaultConnection";
        public ConexionDBContext() : base(ConnectionString)
        {

        }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<MateriaPrima> Materias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Receta> Recetas { get; set; }

  

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasRequired(x => x.Rol).WithMany().HasForeignKey(x => x.RolId);

            base.OnModelCreating(modelBuilder);
        }
    }
}