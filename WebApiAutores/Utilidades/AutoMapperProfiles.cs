using AutoMapper;
using WebApiAutores.Controllers.Entidades;
using WebApiAutores.DTO;

namespace WebApiAutores.Utilidades
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles() 
		{
			//Mappeo de AutorCreacionDTO a Autor
			CreateMap<AutorCreacionDTO, Autor>();
			CreateMap<Autor, AutorDTO>();
			CreateMap<LibroCreacionDTO, Libro>().ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));
			CreateMap<Libro, LibroDTO>();
			CreateMap<ComentarioCreacionDTO, Comentario>();
			CreateMap<Comentario, ComentarioDTO>();
		}

		private List<AutorLibro> MapAutoresLibros(LibroCreacionDTO libroCreacionDTO, Libro libro)
		{
			var resultado = new List<AutorLibro>();

			if(libroCreacionDTO.AutoresIds == null) return resultado;

			foreach(var autorId in libroCreacionDTO.AutoresIds)
			{
				resultado.Add(new AutorLibro() { AutorId = autorId });
			}

			return resultado;
		}
	}
}
