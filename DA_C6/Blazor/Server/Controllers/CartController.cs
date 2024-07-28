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
        //private ICart cart;
        //public CartController(ICart acc) => cart = acc;
        private ApplicationDbContext context;
        public CartController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("GetCarts")]
        public IEnumerable<Cart> GetCarts()
        {
            return context.Carts;
        }
    }
}
