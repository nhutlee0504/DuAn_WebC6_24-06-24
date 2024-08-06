using Blazor.Shared.Model;
using System.Collections.Generic;

namespace Blazor.Server.Services
{
    public interface ICart
    {
        public IEnumerable<Cart> GetAllCart();

        public Cart AddProductToCart(Cart cart);

        public Cart DeleteProductFromCart(int id);

        public Cart UpdateProductFromCart(int id, Cart cart);

        public Cart DeleteAllCartByUsername(string username);
    }
}


