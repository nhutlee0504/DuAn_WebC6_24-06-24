﻿using API.Model;
using System.Collections.Generic;

namespace API.Services
{
    public interface ICart
    {
        public IEnumerable<Cart> GetAllCart();
    }
}
