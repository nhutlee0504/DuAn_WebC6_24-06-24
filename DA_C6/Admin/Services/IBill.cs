using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Admin.Services
{
    public interface IBill
    {
        public IEnumerable<Bill> GetAllBill();

        public Bill GetBillId(int id);
        Task<IEnumerable<Bill>> GetAllBillAsync();
        Task<Bill> UpdateBillStatusAsync(int id, string status);
        Task<Bill> UpdateWithAdminAsync(int id);
        Task<Bill> UpdateWithAdmin2Async(int id);
        Task<Bill> UpdateWithShipperAsync(int id);
        Task<Bill> UpdateWithShipper2Async(int id);
    }
}
