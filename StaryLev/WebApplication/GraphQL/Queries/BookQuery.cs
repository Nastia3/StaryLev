using Application.Services.Interfaces;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using MongoDb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class BookQuery
    {
        [UseOffsetPaging]
        public async Task<IEnumerable<Book>> GetPagingBooksAsync([Service] IBookService BookService) =>
            await BookService.GetAllAsync();

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Book>> GetBooksAsync([Service] IBookService BookService) =>
            await BookService.GetAllAsync();

        [UseProjection]
        public async Task<Book> GetBookByIdAsync(string id, [Service] IBookService BookService) =>
            await BookService.GetByIdAsync(id);

    }
}
