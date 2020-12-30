using AutoMapper;
using Library.Api.Services;
using Library.Contracts.Mapping.Profiles;
using Library.Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection SetupDependencyInjection(this IServiceCollection services)
        {
            // This sets up AutoMapper DI so it's fitting to have it here.
            services.AddAutoMapper(typeof(BooksMappingProfile).Assembly);
            
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<ITagsRepository, TagsRepository>();

            return services;
        }
    }
}