﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Controllers.Entidades;
using WebApiAutores.DTO;

namespace WebApiAutores.Controllers
{
	[ApiController]
	[Route("api/libros/{libroId:int}/comentarios")]
	public class ComentariosController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public ComentariosController(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
		{
			//Verifica si existe el libro
			var existeLibro = await context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);

			if (!existeLibro) return NotFound();

			var comentarios = await context.Comentarios.Where(comentarioDB => comentarioDB.LibroId == libroId).ToListAsync();
			return mapper.Map<List<ComentarioDTO>>(comentarios);
		}

		[HttpPost]
		public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioCreacionDTO)
		{
			//Verifica si existe el libro
			var existeLibro = await context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);

			if (!existeLibro) return NotFound();

			var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
			comentario.LibroId = libroId;
			context.Add(comentario);
			await context.SaveChangesAsync();
			return Ok();
		}
	}
}
