using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public interface ICategory
    {
        public IEnumerable<Category> GetCategories();
        public Category GetCategoryById(int id);
        public Category AddCategory(Category category);
        public Category UpdateCategory(int id,Category category);

    }
}
