using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services
{
    public interface IBill
    {
        public IEnumerable<Bill> GetAllBill();

        public Bill GetBillId(int id);
    }
}
