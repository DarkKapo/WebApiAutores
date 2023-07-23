﻿using AutoMapper;
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
			//Verifica si el autor existe
			//var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
			//if (!existeAutor)
			//{
			//	return BadRequest($"No existe el autor de Id: {libro.AutorId}");
			//}
			//Agrega el libro y graba los cambios

			var libro = mapper.Map<Libro>(libroCreacionDTO);
			context.Add(libro);
			await context.SaveChangesAsync();
			return Ok();
		}
	}
}
