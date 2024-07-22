using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public interface IProduct
    {
        public IEnumerable<Product> GetProducts();

        public Product GetProductId(int id);

        public Product Add(Product product);

        public Product Update(Product product, int id);

        public void Delete(int id);
    }
}
