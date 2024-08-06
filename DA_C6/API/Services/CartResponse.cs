using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
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

        public IEnumerable<Cart> GetAllCart()
        {
            return context.Carts;
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