using System.Collections.Generic;
using System.Linq;

namespace Admin.Data
{
    public class CategoryResponse : ICategory
    {
        private readonly ApplicationDbContext context;
        public CategoryResponse(ApplicationDbContext ct) => context = ct;

        public Category AddCategory(Category category)
        {
            context.categories.Add(category);
            context.SaveChanges();
            return category;
        }

        public IEnumerable<Category> GetCategories()
        {
            return context.categories;
        }

        public Category GetCategoryById(int id)
        {
            return context.categories.FirstOrDefault(x => x.IDCategory == id);
        }

        public Category UpdateCategory(int id, Category category)
        {
            var updateCate = GetCategoryById(category.IDCategory);

            if (updateCate != null)
            {
                updateCate.Name = category.Name;
                context.SaveChanges();
            }

            return updateCate;
        }
    }
}
