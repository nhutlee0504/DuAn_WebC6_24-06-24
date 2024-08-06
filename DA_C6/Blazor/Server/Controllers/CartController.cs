using Blazor.Server.Data;
using Blazor.Server.Services;
using Blazor.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private ICart icart;
        public CartController(ICart acc) => icart = acc;
        //private ApplicationDbContext context;
        //public CartController(ApplicationDbContext context)
        //{
        //    this.context = context;
        //}

        [HttpGet]
        [Route("GetCarts")]
        public IEnumerable<Cart> GetCarts()
        {
            return icart.GetAllCart();
        }

        [HttpPost]
        [Route("AddCart")]
        public Cart AddCart(Cart cart)
        {
            return icart.AddProductToCart(new Cart
            {
                UserName = cart.UserName,
                IDPDetail = cart.IDPDetail,
                Quantity = cart.Quantity,
            });
        }

        [HttpDelete]
        [Route("DeleteCart/{id}")]
        public Cart DeleteCart(int id)
        {
            return icart.DeleteProductFromCart(id);
        }

        [HttpPut]
        [Route("UpdateCart/{id}")]
        public Cart UpdateCart(int id, Cart cart)
        {
            return icart.UpdateProductFromCart(id, cart);
        }

        [HttpDelete]
        [Route("Delete/{username}")]
        public Cart Delete(string username)
        {
            return icart.DeleteAllCartByUsername(username);
        }
    }
}


