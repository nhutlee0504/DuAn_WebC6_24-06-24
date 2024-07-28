using API.Data;
using API.Model;
using System.Collections.Generic;

namespace API.Services
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
