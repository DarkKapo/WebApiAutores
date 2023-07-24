using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Controllers.Entidades
{
	public class Libro
	{
		public int Id { get; set; }
		[Required]
		//Se recomienda poner un máximo para que la base de datos no tenga un nvarchar(max)
		[StringLength(maximumLength: 250)]
		public string Titulo { get; set; }
		public DateTime? FechaPublicacion { get; set; }
		public List<Comentario> Comentarios { get; set; }
		public List<AutorLibro> AutoresLibros { get; set; }
	}
}
