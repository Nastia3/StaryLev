using MongoDb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDb.repository
{
    public class Repository<TModel> : IRepository<TModel> where TModel : IModel, new()
    {
        protected IMongoCollection<TModel> _collection = null;

        public Repository()
        {
            _collection = new MongoContext().Collection<TModel>();
        }
        public Repository(IMongoSettings settings)
        {
            _collection = new MongoContext(settings).Collection<TModel>();
        }

        public async Task<int> InsertOneAsync(TModel model)
        {
            await _collection.InsertOneAsync(model);
            return 1;
        }

        public async Task<int> InsertManyAsync(params TModel[] models)
        {
            await _collection.InsertManyAsync(models);
            return models.Length;

        }

        public async Task<DeleteResult> DeleteByIdAsync(string id)
        {
            return await _collection.DeleteOneAsync(model => model.Id == id);
        }

        public async Task<DeleteResult> DeleteManyAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _collection.DeleteManyAsync(predicate);
        }

        public async Task<DeleteResult> DeleteManyAsync(TModel filter)
        {
            return await _collection.DeleteManyAsync(filter.ToBsonDocument());
        }

        public async Task<DeleteResult> DeleteOneAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _collection.DeleteOneAsync(predicate);
        }

        public async Task<DeleteResult> DeleteOneAsync(TModel filter)
        {
            return await _collection.DeleteOneAsync(filter.ToBsonDocument());
        }

        public async Task<TModel> FindByIdAsync(string id)
        {
            return await _collection.Find(model => model.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TModel>> FindManyAsync(TModel filter)
        {
            return await _collection.Find(filter.ToBsonDocument()).ToListAsync();
        }

        public async Task<IEnumerable<TModel>> FindManyAsync(FilterDefinition<TModel> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<TModel>> FindManyAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task<TModel> FindOneAsync(TModel filter)
        {
            return await _collection.Find(filter.ToBsonDocument()).SingleOrDefaultAsync();
        }

        public async Task<TModel> FindOneAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _collection.Find(predicate).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await _collection.Find(Builders<TModel>.Filter.Empty).ToListAsync();
        }

        public async Task<UpdateResult> UpdateByIdAsync(string id, TModel model)
        {
            return await _collection.UpdateOneAsync(
                model => model.Id == id,
                new BsonDocument("$set", model.ToBsonDocument()));
        }

        public async Task<UpdateResult> UpdateManyAsync(Expression<Func<TModel, bool>> predicate, TModel model)
        {
            return await _collection.UpdateManyAsync(
                predicate,
                new BsonDocument("$set", model.ToBsonDocument()));
        }

        public async Task<UpdateResult> UpdateManyAsync(TModel filter, TModel model)
        {
            return await _collection.UpdateManyAsync(
                filter.ToBsonDocument(),
                new BsonDocument("$set", model.ToBsonDocument()));
        }

        public async Task<UpdateResult> UpdateOneAsync(Expression<Func<TModel, bool>> predicate, TModel model)
        {
            return await _collection.UpdateOneAsync(
                predicate,
                new BsonDocument("$set", model.ToBsonDocument()));
        }

        public async Task<UpdateResult> UpdateOneAsync(TModel filter, TModel model)
        {
            return await _collection.UpdateOneAsync(
                filter.ToBsonDocument(),
                new BsonDocument("$set", model.ToBsonDocument()));
        }

        public async Task<UpdateResult> PushAsync(Expression<Func<TModel, bool>> predicate, string propertyName, string value)
        {
            var update = Builders<TModel>.Update.Push(propertyName, value);
            return await _collection.UpdateOneAsync(predicate, update);
        }

        public async Task<UpdateResult> PushAsync(string id, string propertyName, string value)
        {
            var update = Builders<TModel>.Update.Push(propertyName, value);
            return await _collection.UpdateOneAsync(model => model.Id == id, update);
        }

        public async Task<UpdateResult> PullAsync(Expression<Func<TModel, bool>> predicate, string propertyName, string value)
        {
            var update = Builders<TModel>.Update.Pull(propertyName, value);
            return await _collection.UpdateOneAsync(predicate, update); ;
        }

        public async Task<UpdateResult> PullAsync(string id, string propertyName, string value)
        {
            var update = Builders<TModel>.Update.Pull(propertyName, value);
            return await _collection.UpdateOneAsync(model => model.Id == id, update);
        }
    }
}
