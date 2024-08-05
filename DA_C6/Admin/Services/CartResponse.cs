using Admin.Data;
using Admin.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Services
{
    public class CartResponse : ICart
    {
        private readonly ApplicationDbContext context;
        public CartResponse(ApplicationDbContext ct)
        {
            context = ct;
        }

		public Cart AddProductToCart(Cart cart)
		{
			try
			{
				context.Carts.Add(cart);
				context.SaveChanges();
				return cart;
			}
			catch (System.Exception)
			{

				return null;
			}
		}

		public Cart DeleteAllCart(string user)
		{
			var timsp = context.Carts.Find(user);
			if (timsp != null)
			{
				context.Carts.Remove(timsp);
				context.SaveChanges();
				return timsp;
			}
			return null;
		}

		public Cart DeleteProductFromCart(int id)
		{
			var cartItem = context.Carts.Find(id);
			if (cartItem != null)
			{
				context.Carts.Remove(cartItem);
				context.SaveChanges();
				return cartItem;
			}
			return null; // Trả về null nếu không tìm thấy sản phẩm trong giỏ hàng
		}

		public IEnumerable<Cart> GetAllCart()
        {
            return context.Carts;
        }

		[HttpPut]
		[Route("UpdateCart/{id}")]
		public Cart UpdateCart(int id, Cart cart)
		{
			try
			{
				var c = context.Carts.Find(id);
				if (c != null)
				{
					c.Quantity = cart.Quantity;
					context.SaveChanges();
					return cart;
				}
				return null;
			}
			catch (System.Exception)
			{

				return null;
			}
		}

		public Cart UpdateProductFromCart(int id, Cart cart)
		{
			throw new System.NotImplementedException();
		}
	}
}
