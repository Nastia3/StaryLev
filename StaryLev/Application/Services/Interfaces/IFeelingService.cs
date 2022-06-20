using MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IFeelingService
    {
        Task<Book> LikeAsync(string bookId, string nickname);
        Task<Book> DislikeAsync(string bookId, string nickname);
    }
}
