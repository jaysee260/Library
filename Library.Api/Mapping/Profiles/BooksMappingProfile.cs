using AutoMapper;
using Library.Contracts.RestApi;
using Library.Data.Entities;

namespace Library.Api.Mapping.Profiles
{
    public class BooksMappingProfile : Profile
    {
        public BooksMappingProfile()
        {
            CreateMap<AuthorDto, Author>().ReverseMap();
            CreateMap<BookDto, Book>().ReverseMap();
        }
    }
}