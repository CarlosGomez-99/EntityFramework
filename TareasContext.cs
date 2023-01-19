using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyectef.Models;

namespace proyectef
{
    public class TareasContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            List<Categoria> categoriasInit = new List<Categoria>();
            categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("28ff6498-da17-4450-be86-76143bc3d6ca"), Nombre = "Actividades pendientes", Peso = 20 });
            categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("1d34be34-e05a-40d7-92ee-87a9171f613d"), Nombre = "Actividades personales", Peso = 50 });


            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);

                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
                categoria.Property(p => p.Descripcion).IsRequired(false);

                categoria.Property(p => p.Peso);

                categoria.HasData(categoriasInit);
            });

            List<Tarea> tareasInit = new List<Tarea>();
            tareasInit.Add(new Tarea() { TareaId = Guid.Parse("e1403e74-8c40-4463-b867-9d399ce11eca"), CategoriaId = Guid.Parse("28ff6498-da17-4450-be86-76143bc3d6ca"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios", FechaCreacion = DateTime.Now });
            tareasInit.Add(new Tarea() { TareaId = Guid.Parse("dc1ded32-239c-41e8-8803-1e0353194252"), CategoriaId = Guid.Parse("1d34be34-e05a-40d7-92ee-87a9171f613d"), PrioridadTarea = Prioridad.Alta, Titulo = "Pago de matricula Platzi", FechaCreacion = DateTime.Now });

            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.ToTable("Tarea");

                tarea.HasKey(p => p.TareaId);

                tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

                tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(p => p.Descripcion).IsRequired(false);
                tarea.Property(p => p.PrioridadTarea);
                tarea.Property(p => p.FechaCreacion);
                tarea.Ignore(p => p.Resumen);

                tarea.HasData(tareasInit);
            });
        }
    }
}