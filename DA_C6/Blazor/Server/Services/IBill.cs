using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public interface IBill
    {
        public IEnumerable<Bill> GetAllBill();

        public Bill GetBillId(int id);
    }
}
