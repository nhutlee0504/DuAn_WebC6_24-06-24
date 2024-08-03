using Admin.Model;
using System.Collections.Generic;

namespace Admin.Services
{
    public interface ICart
    {
        public IEnumerable<Cart> GetAllCart();

		public Cart AddProductToCart(Cart cart);

        public Cart DeleteProductFromCart(int id);

        public Cart UpdateProductFromCart(int id, Cart cart);

        public Cart DeleteAllCart(string user);
	}
}
