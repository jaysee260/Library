using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Contracts.RestApi;

namespace Library.Api.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync(int resultsPerPage, int offset);
        Task<BookDto> AddBook(BookDto book);
        Task<BookDto> GetBook(Guid id);
        Task RemoveBookAsync(Guid id);
    }
}