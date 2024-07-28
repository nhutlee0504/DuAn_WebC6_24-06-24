using Blazor.Server.Data;
using Blazor.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Server.Services
{
    public class CartResponse : ICart
    {
        private readonly ApplicationDbContext context;
        public CartResponse(ApplicationDbContext ct)
        {
            context = ct;
        }

        public IEnumerable<Cart> GetAllCart()
        {
            return context.Carts;
        }
    }
}
