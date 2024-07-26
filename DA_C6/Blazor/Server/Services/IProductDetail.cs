using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
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
