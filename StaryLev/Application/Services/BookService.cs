using Application.Services.Interfaces;
using MongoDb.Models;
using MongoDb.repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService : IBookService, IFeelingService
    {
        readonly IRepository<Book> _bookRepository;
        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> DislikeAsync(string bookId, string nickname)
        {
            var book = await _bookRepository.FindByIdAsync(bookId);
            if (book is not null && !book.DislikeUserNicknames.Contains(nickname))
            {
                if (book.LikeUserNicknames.Contains(nickname))
                {
                    book.LikeUserNicknames.Remove(nickname);
                    await _bookRepository.PullAsync(m => m.Id == book.Id, "LikeUserNicknames", nickname);
                }
                book.DislikeUserNicknames.Add(nickname);
                await _bookRepository.PushAsync(m => m.Id == book.Id, "DislikeUserNicknames", nickname);
            }
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            return await _bookRepository.FindByIdAsync(id);
        }

        public async Task<Book> LikeAsync(string bookId, string nickname)
        {
            var book = await _bookRepository.FindByIdAsync(bookId);
            if(book is not null && !book.LikeUserNicknames.Contains(nickname))
            {
                if (book.DislikeUserNicknames.Contains(nickname))
                {
                    book.DislikeUserNicknames.Remove(nickname);
                    await _bookRepository.PullAsync(m => m.Id == book.Id, "DislikeUserNicknames", nickname);
                }
                book.LikeUserNicknames.Add(nickname);
                await _bookRepository.PushAsync(m => m.Id == book.Id, "LikeUserNicknames", nickname);
            }
            return book;         
        }
    }
}
