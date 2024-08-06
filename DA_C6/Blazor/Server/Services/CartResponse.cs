using Blazor.Server.Data;
using Blazor.Shared.Model;
using Microsoft.AspNetCore.Mvc;
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

        public Cart UpdateProductFromCart(int id, Cart cart)
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

        public Cart DeleteAllCartByUsername(string username)
        {
            var cartsToRemove = context.Carts.Where(c => c.UserName == username).ToList();
            if (cartsToRemove != null)
            {
                context.Carts.RemoveRange(cartsToRemove);
                context.SaveChanges();
                return cartsToRemove.FirstOrDefault(); // Trả về một đối tượng Cart nếu có
            }
            return null; // Trả về null nếu không có sản phẩm nào được xóa
        }
    }
}


