using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Controllers.Entidades
{
	public class Autor
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(maximumLength: 120, ErrorMessage = "El campo {0} permite hasta {1} letras")]
		public string Nombre { get; set; }

	}
}