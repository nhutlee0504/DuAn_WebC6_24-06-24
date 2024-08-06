using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IProduct
    {
        public IEnumerable<Product> GetProducts();

        public Product GetProductId(int id);
        Task<IEnumerable<Product>> SearchProductsByNameAsync(string name);

        public Product Add(Product product);

        public Product Update(Product product, int id);

        public void Delete(int id);
    }
}
