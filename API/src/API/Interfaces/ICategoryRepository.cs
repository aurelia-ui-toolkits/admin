using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> All();
        Task Create(Category category);
    }
}