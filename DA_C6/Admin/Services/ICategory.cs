using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Model;

namespace Admin.Services
{
    public interface ICategory
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(int id, Category category);
    }
}
