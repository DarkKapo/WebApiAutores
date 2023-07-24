using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
		[HttpGet("{id:int}", Name = "ObtenerLibro")]
		public async Task<ActionResult<LibroDTOConAutores>> Get(int id)
		{
			var libro = await context.Libros.Include(libroDB => libroDB.AutoresLibros).ThenInclude(autorLibroDB => autorLibroDB.Autor).FirstOrDefaultAsync(x => x.Id == id);

			if (libro == null) return NotFound();

			//Para mantener el orden con el que se registró cada autor
			libro.AutoresLibros = libro.AutoresLibros.OrderBy(x => x.Orden).ToList();

			return mapper.Map<LibroDTOConAutores>(libro);
		}

		[HttpPost]
		public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
		{
			if (libroCreacionDTO == null) return BadRequest("No se puede crear un libro sin autores");
			//Verifica que todos los autores enviados existan
			var autoresIds = await context.Autores.Where(autorDB => libroCreacionDTO.AutoresIds.Contains(autorDB.Id)).ToListAsync();

			if (libroCreacionDTO.AutoresIds.Count != autoresIds.Count) return BadRequest("No existe uno de los autores");

			var libro = mapper.Map<Libro>(libroCreacionDTO);
			AsignarOrdenAutores(libro);

			context.Add(libro);
			await context.SaveChangesAsync();

			var libroDTO = mapper.Map<LibroDTO>(libro);
			return CreatedAtRoute("ObtenerLibro", new { id = libro.Id }, libroDTO);
		}

		[HttpPut]
		public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO)
		{
			//Trae el libro (para ver si existe) y AutoresLibros (para actualizarlo)
			var librodDB = await context.Libros.Include(x => x.AutoresLibros).FirstOrDefaultAsync(x => x.Id == id);

			if (librodDB == null) return NotFound();

			//Mapear de liboCreacionDTO a libroDB para que libroDB tenga la misma estructa y así poder realizar el PUT
			librodDB = mapper.Map(libroCreacionDTO, librodDB);
			AsignarOrdenAutores(librodDB);

			await context.SaveChangesAsync();
			return NoContent();
		}

		private void AsignarOrdenAutores(Libro libro)
		{
			//Cuando se guarde un libro, se insertan en el mismo orden que se envían
			if (libro.AutoresLibros != null)
				for (int i = 0; i < libro.AutoresLibros.Count; i++)
					libro.AutoresLibros[i].Orden = i;
		}

		[HttpPatch("{id:int}")]
		public async Task<ActionResult> Patch(int id, JsonPatchDocument<LibroPatchDTO> patchDocument)
		{
			if(patchDocument == null) return BadRequest();

			var libroDb = await context.Libros.FirstOrDefaultAsync(x => x.Id == id);

			if(libroDb == null) return NotFound();

			//Llena LibroPatchDTO con la info de libroDb
			var libroDTO = mapper.Map<LibroPatchDTO>(libroDb);
			//Aplica al libro DTO los cambios que vienen de patchDocument
			patchDocument.ApplyTo(libroDTO, ModelState); //Guarda los cambios

			//Verifica que las reglas de validacion se cumple
			var esValido = TryValidateModel(libroDTO);

			if(!esValido) return BadRequest(ModelState);

			mapper.Map(libroDTO, libroDb);

			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			//Verifica si existe el id
			var existe = await context.Libros.AnyAsync(x => x.Id == id);

			if (!existe) return NotFound();

			//Instancia un autor para poder borrar
			context.Remove(new Libro() { Id = id });
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
