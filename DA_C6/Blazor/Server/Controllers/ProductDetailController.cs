using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blazor.Shared.Model;
using Blazor.Server.Services;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private IProductDetail productDetail;
        public ProductDetailController(IProductDetail prodDt) => productDetail = prodDt;

        [HttpGet]
        [Route("GetProductDetails")]
        public IEnumerable<ProductDetails> GetProductDetails()
        {
            return productDetail.GetProductDetails();
        }


        [HttpGet("id")]
        [Route("GetPddtByProdId/{id}")]
        public IEnumerable<ProductDetails> GetPddtByProdId(int id)
        {
            return productDetail.GetPddtByProdId(id);
        }

        [HttpGet("{id}")]
        public ProductDetails GetDetails(int id)
        {
            return productDetail.GetProductDetails(id);
        }

        [HttpPost]
        public ProductDetails Add(ProductDetails prodDt)
        {
            return productDetail.Add(new ProductDetails
            {
                IDColor = prodDt.IDColor,
                IDProduct = prodDt.IDProduct,
                Size = prodDt.Size,
                Quantity = prodDt.Quantity
            });
        }

        [HttpPut("{id}")]
        public ProductDetails Update(ProductDetails prodDt, int id)
        {
            return productDetail.Update(prodDt, id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productDetail.Delete(id);
        }
    }
}
