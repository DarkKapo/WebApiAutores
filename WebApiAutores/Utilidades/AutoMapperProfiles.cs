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
		}
	}
}
