using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.Repositories
{
    public class BaseRepository<T>
    {
        private readonly IMongoCollection<T> _collection;

        public BaseRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<List<T>> All()
        {
            var filter = new BsonDocument();
            var items = new List<T>();
            using (var cursor = await _collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        items.Add(document);
                    }
                }
            }

            return items;
        }

        public async Task Create(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
    }
}