namespace WebApiAutores.Controllers.Entidades
{
	public class AutorLibro
	{
		public int LibroId { get; set; }
		public int AutorId { get; set; }
		//DESC o ASC
		public int Orden { get; set;}
		public Libro Libro { get; set; }
		public Autor Autor { get; set; }
	}
}
