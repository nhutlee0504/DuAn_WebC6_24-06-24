using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services
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
