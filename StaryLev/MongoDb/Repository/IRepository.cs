using MongoDb.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDb.repository
{
    public interface IRepository<TModel> where TModel : IModel, new()
    {
        Task<int> InsertOneAsync(TModel model);
        Task<int> InsertManyAsync(params TModel[] models);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> FindByIdAsync(string id);
        Task<TModel> FindOneAsync(TModel filter);
        Task<TModel> FindOneAsync(Expression<Func<TModel, bool>> predicate);
        Task<IEnumerable<TModel>> FindManyAsync(TModel filter);
        Task<IEnumerable<TModel>> FindManyAsync(Expression<Func<TModel, bool>> predicate);
        Task<IEnumerable<TModel>> FindManyAsync(FilterDefinition<TModel> filter);
        Task<DeleteResult> DeleteByIdAsync(string id);
        Task<DeleteResult> DeleteOneAsync(Expression<Func<TModel, bool>> predicate);
        Task<DeleteResult> DeleteOneAsync(TModel filter);
        Task<DeleteResult> DeleteManyAsync(Expression<Func<TModel, bool>> predicate);
        Task<DeleteResult> DeleteManyAsync(TModel filter);
        Task<UpdateResult> UpdateByIdAsync(string id, TModel model);
        Task<UpdateResult> UpdateOneAsync(Expression<Func<TModel, bool>> predicate, TModel model);
        Task<UpdateResult> UpdateOneAsync(TModel filter, TModel model);
        Task<UpdateResult> UpdateManyAsync(Expression<Func<TModel, bool>> predicate, TModel model);
        Task<UpdateResult> UpdateManyAsync(TModel filter, TModel model);
        Task<UpdateResult> PushAsync(Expression<Func<TModel, bool>> predicate, string propertyName, string value);
        Task<UpdateResult> PushAsync(string id, string propertyName, string value);
        Task<UpdateResult> PullAsync(Expression<Func<TModel, bool>> predicate, string propertyName, string value);
        Task<UpdateResult> PullAsync(string id, string propertyName, string value);
    }
}

