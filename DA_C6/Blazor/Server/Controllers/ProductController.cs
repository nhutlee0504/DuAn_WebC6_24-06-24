using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blazor.Shared.Model;
using Blazor.Server.Services;
using System.Threading.Tasks;
using System.Linq;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProduct product;
        public ProductController(IProduct prod)
        {
            product = prod;
        }
        [HttpGet("search")]
        [Route("SearchProductsByName/{name}")]
        public ActionResult<IEnumerable<Product>> SearchProductsByName(string name)
        {
            var products = product.SearchProductsByNameAsync(name);

            if (products == null || !products.Any())
            {
                return NotFound("No products found with the specified name.");
            }

            return Ok(products);
        }


        [HttpGet]
        [Route("GetProducts")]
        public IEnumerable<Product> GetProducts()
        {
            return product.GetProducts();
        }

        [HttpPost]
        public Product Add(Product prod)
        {
            return product.Add(new Product
            {
                IDSupplier = prod.IDSupplier,
                IDCategory = prod.IDCategory,
                Name = prod.Name,
                Price = prod.Price,
                Image = prod.Image,
                Describe = prod.Describe,
                Status = prod.Status
            });
        }

        [HttpGet("{id}")]
        [Route("GetProduct/{id}")]
        public Product GetProduct(int id)
        {
          return product.GetProductId(id);
        }

        [HttpPut("{id}")]
        public Product Update(Product prod, int id)
        {
            return product.Update(prod, id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            product.Delete(id);
        }
    }
}
