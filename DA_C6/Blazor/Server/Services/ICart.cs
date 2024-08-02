using Blazor.Shared.Model;
using System.Collections.Generic;

namespace Blazor.Server.Services
{
    public interface ICart
    {
        public IEnumerable<Cart> GetAllCart();
    }
}
