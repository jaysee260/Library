using System;
using System.Threading.Tasks;
using Library.Contracts.RestApi;

namespace Library.Api.Services
{
    public interface IBooksService
    {
        Task<BookDto> AddBook(BookDto book);
        Task<BookDto> GetBook(Guid id);
        Task RemoveBookAsync(Guid id);
    }
}