using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Controllers.Entidades;
using WebApiAutores.DTO;

namespace WebApiAutores.Controllers
{
	[ApiController]
	[Route("api/libros")]
	public class LibrosController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public LibrosController(ApplicationDbContext context, IMapper mapper) 
		{
			this.context = context;
			this.mapper = mapper;
		}
		//Trae un libro en específico
		[HttpGet("{id:int}")]
		public async Task<ActionResult<LibroDTO>> Get(int id)
		{
			var libro = await context.Libros.Include(libroDB => libroDB.Comentarios).FirstOrDefaultAsync(x => x.Id == id);
			return mapper.Map<LibroDTO>(libro);
		}

		[HttpPost]
		public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
		{
			if (libroCreacionDTO == null) return BadRequest("No se puede crear un libro sin autores");
			//Verifica que todos los autores enviados existan
			var autoresIds = await context.Autores.Where(autorDB => libroCreacionDTO.AutoresIds.Contains(autorDB.Id)).ToListAsync();

			if (libroCreacionDTO.AutoresIds.Count != autoresIds.Count) return BadRequest("No existe uno de los autores");

			var libro = mapper.Map<Libro>(libroCreacionDTO);
			//Cuando se guarde un libro, se insertan en el mismo orden que se env'ian
			if(libro.AutoresLibros != null)
				for (int i = 0; i < libro.AutoresLibros.Count; i++)
					libro.AutoresLibros[i].Orden = i;

			context.Add(libro);
			await context.SaveChangesAsync();
			return Ok();
		}
	}
}
