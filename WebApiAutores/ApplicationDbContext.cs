﻿using Microsoft.EntityFrameworkCore;
using WebApiAutores.Controllers.Entidades;

namespace WebApiAutores
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AutorLibro>().HasKey(al => new { al.AutorId, al.LibroId });
		}
		//Crea la tabla Autores de la entidad Autor
		public DbSet<Autor> Autores { get; set; }
		public DbSet<Libro> Libros { get; set; }
		public DbSet<Comentario> Comentarios { get; set; }
		public DbSet<AutorLibro> AutoresLibros { get; set;}
	}
}
