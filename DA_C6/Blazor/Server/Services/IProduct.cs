using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blazor.Server.Services
{
    public interface IProduct
    {
        public IEnumerable<Product> GetProducts();

        public Product GetProductId(int id);

        public Product Add(Product product);
        public IEnumerable<Product> SearchProductsByNameAsync(string name);
        public Product Update(Product product, int id);

        public void Delete(int id);
    }
}
