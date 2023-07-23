using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTO
{
	public class LibroCreacionDTO
	{
		[StringLength(maximumLength: 250)]
		public string Titulo { get; set; }
		public List<int> AutoresIds { get; set; }
	}
}
