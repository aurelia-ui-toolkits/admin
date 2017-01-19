using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.MongoDB;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        private UserManager<IdentityUser> um;

        public CategoryController(ICategoryRepository categoryRepo, UserManager<IdentityUser> um)
        {
            _categoryRepo = categoryRepo;

            // used this in Get() to create a test user, confirmed that this test user
            // was added to the mongodb database
            this.um = um;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            await _categoryRepo.Create(category);

            return Ok(category);
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _categoryRepo.All();
        }
    }
}
