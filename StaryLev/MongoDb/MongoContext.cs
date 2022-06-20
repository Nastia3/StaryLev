using MongoDb.Models;
using MongoDB.Driver;
using System;
using System.Configuration;
using System.Security.Authentication;

namespace MongoDb
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext()
        {
            string localConnectionString = ConfigurationManager.AppSettings.Get("connectionString");            
            var client = new MongoClient(localConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase("StaryLev");
            }
        }
        public MongoContext(IMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.DatabaseName);
            }
        }

        public IMongoCollection<TEntity> Collection<TEntity>() where TEntity : IModel => typeof(TEntity).Name switch
        {
            nameof(Book) => _database.GetCollection<TEntity>("Books"),
            nameof(User) => _database.GetCollection<TEntity>("Users"),
            _ => throw new Exception()
        };
    }
}
