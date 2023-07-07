﻿using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Controllers.Entidades
{
	public class Libro
	{
		public int Id { get; set; }
		//Se recomienda poner un máximo para que la base de datos no tenga un nvarchar(max)
		[StringLength(maximumLength: 250)]
		public string Titulo { get; set; }
	}
}
