using AutoMapper;
using WebAPi.Dtos;
using WebAPi.Entities;

namespace WebAPi.Profil
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorCreationDto, Author>();
            
        }
    }
}
