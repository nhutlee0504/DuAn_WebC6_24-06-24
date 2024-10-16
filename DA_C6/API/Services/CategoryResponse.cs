﻿using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class CategoryResponse : ICategory
    {
        private readonly ApplicationDbContext context;
        public CategoryResponse(ApplicationDbContext ct) => context = ct;

        public Category AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        public IEnumerable<Category> GetCategories()
        {
            return context.Categories;
        }

        public Category GetCategoryById(int id)
        {
            return context.Categories.FirstOrDefault(x => x.IDCategory == id);
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
