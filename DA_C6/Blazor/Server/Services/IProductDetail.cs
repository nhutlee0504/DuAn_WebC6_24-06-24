using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public interface IProductDetail
    {
        public IEnumerable<ProductDetails> GetProductDetails();

        public IEnumerable<ProductDetails> GetPddtByProdId(int productId);

        public ProductDetails GetProductDetails(int id);

        public ProductDetails Add(ProductDetails productDetails);

        public ProductDetails Update(ProductDetails productDetails, int id);

        public void Delete(int id);
    }
}
