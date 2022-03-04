using AutoMapper;
using WebAPi.Dtos;
using WebAPi.Entities;

namespace WebAPi.Profil
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
