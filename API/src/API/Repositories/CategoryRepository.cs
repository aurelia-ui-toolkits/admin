using API.Models;
using MongoDB.Driver;

namespace API.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoDatabase database) : base(database.GetCollection<Category>("Categories"))
        {
        }
    }
}
