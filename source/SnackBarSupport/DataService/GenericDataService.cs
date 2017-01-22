using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Dto;
using MongoDB.Driver;
using SnackBarSupport.DAL;

namespace SnackBarSupport.DataService
{
    public class GenericDataService<T> where T : DtoBase
    {
        protected MongoDbContext MongoDbContext;

        public async Task AddAsync(T entity)
        {
            await MongoDbContext
                .GetCollection<T>()
                .InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await MongoDbContext
                .GetCollection<T>()
                .DeleteOneAsync(i => i.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            await MongoDbContext
                .GetCollection<T>()
                .ReplaceOneAsync(i => i.Id == entity.Id, entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result =  await MongoDbContext.GetCollection<T>()
                .FindAsync(_ => true);

            return result.ToList();
        }

        public async Task<T> GetAsync(string id)
        {
            return await MongoDbContext.GetCollection<T>()
                .Find(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}