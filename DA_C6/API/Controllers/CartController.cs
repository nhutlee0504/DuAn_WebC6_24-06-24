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
        private ICart cart;
        public CartController(ICart acc) => cart = acc;

        [HttpGet]
        public IEnumerable<Cart> GetAll()
        {
            return cart.GetAllCart();
        }
    }
}
