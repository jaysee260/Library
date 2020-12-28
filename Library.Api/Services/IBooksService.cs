using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Contracts.Common;
using Library.Contracts.RestApi;

namespace Library.Api.Services
{
    public interface IBooksService
    {
        Task<BookDto> AddBookAsync(BookDto book);
        Task<BookDto> GetBookAsync(Guid id);
        Task RemoveBookAsync(Guid id);
        Task<int> GetBooksCountAsync();
        Task<IEnumerable<BookDto>> GetAllBooksAsync(int resultsPerPage, int offset, OrderBy orderBy);
    }
}