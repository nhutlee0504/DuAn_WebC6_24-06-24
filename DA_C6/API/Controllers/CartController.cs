using API.Model;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private ICart icart;
        public CartController(ICart acc) => icart = acc;

        [HttpGet]
        public IEnumerable<Cart> GetAll()
        {
            return icart.GetAllCart();
        }

        [HttpPost]
        public Cart Add(Cart cart)
        {
            return icart.AddProductToCart(new Cart
            {
                UserName = cart.UserName,
                IDPDetail = cart.IDPDetail,
                Quantity = cart.Quantity,
            });
        }

        [HttpDelete]
        public Cart Delete(string username)
        {
            return icart.DeleteAllCartByUsername(username);
        }
    }
}