using AutoMapper;
using APIGestionNotas.Models;

namespace APIGestionNotas.Helpers
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() 
        { 
            CreateMap<Nota, NotaDTO>().ReverseMap();
            CreateMap<Nota, UpdateNotaDTO>().ReverseMap();

        }

    }
}
