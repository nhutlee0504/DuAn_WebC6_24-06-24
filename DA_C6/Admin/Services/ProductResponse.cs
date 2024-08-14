using Admin.Data;
using Admin.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Services
{
    public class ProductResponse : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductResponse(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product, int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Image = product.Image;
                existingProduct.Status = product.Status;
                existingProduct.IDSupplier = product.IDSupplier;
                existingProduct.IDCategory = product.IDCategory;

                // Ensure you update the product
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
            return existingProduct;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsWithDetailsAsync()
        {
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd.Colors)
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd.Sizes)
                .ToListAsync();
        }
    }
}
