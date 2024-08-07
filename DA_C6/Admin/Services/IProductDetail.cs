using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Admin.Services
{
    public interface IProductDetail
    {
        Task<IEnumerable<ProductDetails>> GetProductDetailsAsync();

        Task<IEnumerable<ProductDetails>> GetPddtByProdIdAsync(int productId);

        Task<ProductDetails> GetProductDetailsAsync(int id);

        Task<ProductDetails> AddAsync(ProductDetails productDetails);

        Task<ProductDetails> UpdateAsync(ProductDetails productDetails, int id);

        Task DeleteAsync(int id);
    }
}
