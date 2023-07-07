using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiAutores.Controllers.Entidades;

namespace WebApiAutores.Controllers
{
	//Permite hacer validaciones automática con la data recibida
	[ApiController]
	[Route("api/autores")]
	public class AutoresController : ControllerBase
	{
		//Traer ApplicationDbContext para hacer conexión a la BD
		private readonly ApplicationDbContext context;

		public AutoresController(ApplicationDbContext context) 
		{
			this.context = context;
		}

		[HttpGet] // ruta = api/autores
		public async Task<List<Autor>> Get() 
		{	//Retorna una lista de autores
			return await context.Autores.ToListAsync();
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Autor>> Get(int id)
		{
			var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

			if(autor == null) return NotFound();

			return autor;
		}

		[HttpPost]
		public async Task<ActionResult> Post(Autor autor)
		{
			var existeAutor = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

			if (existeAutor) return BadRequest($"Ya existe el autor {autor.Nombre}");

			context.Add(autor);
			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("{id:int}")] // ruta = api/autores/id
		public async Task<ActionResult> Put(Autor autor, int id)
		{
			if (autor.Id != id) return BadRequest("Id no coincide");

			//Verifica si existe el id
			var existe = await context.Autores.AnyAsync(x => x.Id == id);

			if (!existe) return NotFound();

			context.Update(autor);
			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			//Verifica si existe el id
			var existe = await context.Autores.AnyAsync(x => x.Id == id);

			if(!existe) return NotFound();

			//Instancia un autor para poder borrar
			context.Remove(new Autor () { Id = id });
			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
