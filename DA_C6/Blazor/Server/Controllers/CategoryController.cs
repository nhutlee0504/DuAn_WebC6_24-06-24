using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blazor.Shared.Model;
using Blazor.Server.Services;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategory category;
        public CategoryController(ICategory cgr) => category = cgr;

        [HttpGet]
        [Route("GetCategories")]
        public IEnumerable<Category> GetCategories()
        {
            return category.GetCategories();
        }

        [HttpGet("{id}")]
        public Category GetCategory(int id)
        {
            return category.GetCategoryById(id);
        }

        [HttpPost]
        public Category PostCategory(Category cacte)
        {
            return category.AddCategory(new Category
            {
                Name = cacte.Name,
            });
        }

        [HttpPut("{id}")]
        public Category Update(int id, Category cate)
        {
            var tim = category.GetCategoryById(id);
            tim.Name = cate.Name;
            category.UpdateCategory(id, tim);
            return tim;
            
        }
    }
}
