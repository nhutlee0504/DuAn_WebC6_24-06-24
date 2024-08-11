using Admin.Data;
using Admin.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Services
{
    public class CategoryResponse : ICategory
    {
        private readonly ApplicationDbContext _context;

        public CategoryResponse(ApplicationDbContext context) => _context = context;

        public async Task<Category> AddCategoryAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (System.Exception ex)
            {
                // Log exception (ex.Message) here
                return null;
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await GetCategoryByIdAsync(id);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;

                try
                {
                    _context.Categories.Update(existingCategory);
                    await _context.SaveChangesAsync();
                    return existingCategory;
                }
                catch (System.Exception ex)
                {
                    // Log exception (ex.Message) here
                    return null;
                }
            }

            return null;
        }
    }
}
