using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Controllers.Entidades;

namespace WebApiAutores.Controllers
{
	[ApiController]
	[Route("api/libros")]
	public class LibrosController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public LibrosController(ApplicationDbContext context) 
		{
			this.context = context;
		}
		////Trae un libro en específico
		//[HttpGet("{id:int}")]
		//public async Task<ActionResult<Libro>> Get(int id)
		//{
		//	return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
		//}

		//[HttpPost]
		//public async Task<ActionResult> Post(Libro libro)
		//{
		//	//Verifica si el autor existe
		//	var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
		//	if (!existeAutor)
		//	{
		//		return BadRequest($"No existe el autor de Id: {libro.AutorId}");
		//	}
		//	//Agrega el libro y graba los cambios
		//	context.Add(libro);
		//	await context.SaveChangesAsync();
		//	return Ok();
		//}
	}
}
