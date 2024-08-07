using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Admin.Services
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> GetProductsAsync(); // Sửa phương thức này
        Task<Product> GetProductByIdAsync(int id); // Sửa phương thức này
        Task<Product> AddAsync(Product product); // Sửa phương thức này
        Task<Product> UpdateAsync(Product product, int id); // Sửa phương thức này
        Task DeleteAsync(int id); // Sửa phương thức này
        Task<IEnumerable<Product>> GetProductsWithDetailsAsync(); 
    }
}
