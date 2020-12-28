using AutoMapper;
using Library.Contracts.RestApi;
using Library.Contracts.DatabaseEntities;

namespace Library.Contracts.Mapping.Profiles
{
    public class BooksMappingProfile : Profile
    {
        public BooksMappingProfile()
        {
            CreateMap<AuthorDto, Author>().ReverseMap();
            CreateMap<PublisherDto, Publisher>().ReverseMap();
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<TagDto, Tag>().ReverseMap();
            CreateMap<BookDto, Book>().ReverseMap();
        }
    }
}