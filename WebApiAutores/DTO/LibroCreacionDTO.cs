﻿using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTO
{
	public class LibroCreacionDTO
	{
		[StringLength(maximumLength: 250)]
		[Required]
		public string Titulo { get; set; }
		public DateTime FechaPublicacion { get; set; }
		public List<int> AutoresIds { get; set; }
	}
}
