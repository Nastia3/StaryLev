using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiServices.Models;
using System.Reactive.Linq;
using Akavache;

namespace ConsoleApp1
{
    public class BlobCacheService
    {
        private const string applicationName = "BookBlobStorage";
        public BlobCacheService()
        {
            Registrations.Start(applicationName);
        }

        public async Task CacheCurrentCollection(List<Book> books, string index)
        {
            await BlobCache.UserAccount.InsertObject(index,books);
        }

        public async Task<List<Book>> GetCollection(string index)
        {
            return await BlobCache.UserAccount.GetObject<List<Book>>(index);
        }
    }
}
