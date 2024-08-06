using API.Model;
using System.Collections.Generic;

namespace API.Services
{
    public interface ICart
    {
        public IEnumerable<Cart> GetAllCart();

        public Cart AddProductToCart(Cart cart);

        public Cart DeleteAllCartByUsername(string username);

    }
}