using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Data;
using Admin.Model;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services
{
    public class ProductDetailResponse : IProductDetail
    {
        private readonly ApplicationDbContext context;

        public ProductDetailResponse(ApplicationDbContext ct) => context = ct;

        public async Task<ProductDetails> AddAsync(ProductDetails productDetails)
        {
            context.ProductDetails.Add(productDetails);
            await context.SaveChangesAsync();
            return productDetails;
        }

        public async Task DeleteAsync(int id)
        {
            var prod = await context.ProductDetails.FirstOrDefaultAsync(x => x.IDPDetail == id);
            if (prod != null)
            {
                context.ProductDetails.Remove(prod);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductDetails>> GetPddtByProdIdAsync(int productId)
        {
            return await context.ProductDetails
                                .Include(pd => pd.Colors)
                                .Include(pd => pd.Sizes)
                                .Where(x => x.IDProduct == productId)
                                .ToListAsync();
        }

        public async Task<IEnumerable<ProductDetails>> GetProductDetailsAsync()
        {
            return await context.ProductDetails
                                .Include(pd => pd.Colors)
                                .Include(pd => pd.Sizes)
                                .ToListAsync();
        }

        public async Task<ProductDetails> GetProductDetailsAsync(int id)
        {
            return await context.ProductDetails
                                .Include(pd => pd.Colors)
                                .Include(pd => pd.Sizes)
                                .FirstOrDefaultAsync(x => x.IDPDetail == id);
        }

        public async Task<ProductDetails> UpdateAsync(ProductDetails productDetails, int id)
        {
			var existingProduct = await context.ProductDetails.FindAsync(id);
			if (existingProduct != null)
			{
				existingProduct.Size = productDetails.Size;
				existingProduct.IDColor = productDetails.IDColor;
                existingProduct.Quantity = productDetails.Quantity;
				await context.SaveChangesAsync();
			}
			return existingProduct;
			//try
   //         {
   //             var prod = await context.ProductDetails.FirstOrDefaultAsync(x => x.IDPDetail == id);
   //             if (prod != null)
   //             {
   //                 prod.IDColor = productDetails.IDColor;
   //                 prod.IDProduct = productDetails.IDProduct;
   //                 prod.Size = productDetails.Size;
   //                 prod.Quantity = productDetails.Quantity;
   //                 await context.SaveChangesAsync();
   //                 return productDetails;
   //             }
   //             return null;
   //         }
   //         catch
   //         {
   //             return null;
   //         }
        }
    }
}
