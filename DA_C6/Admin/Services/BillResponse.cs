using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Admin.Services
{
    public class BillResponse : IBill
    {
        private readonly ApplicationDbContext _context;
        public BillResponse(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Bill>> GetAllBillAsync()
        {
            return await _context.Bills.ToListAsync();
        }
        public IEnumerable<Bill> GetAllBill()
        {
            return _context.Bills;
        }

        public Bill GetBillId(int id)
        {
            return _context.Bills.FirstOrDefault(x => x.IDBill == id);
        }
        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await _context.Bills.FirstOrDefaultAsync(x => x.IDBill == id);
        }
        public async Task<Bill> UpdateBillStatusAsync(int id, string status)
        {
            var bill = await GetBillByIdAsync(id);
            if (bill != null)
            {
                bill.Status = status;
                await _context.SaveChangesAsync();
            }
            return bill;
        }

        public Task<Bill> UpdateWithAdminAsync(int id)
        {
            return UpdateBillStatusAsync(id, "Xác nhận");
        }

        public Task<Bill> UpdateWithAdmin2Async(int id)
        {
            return UpdateBillStatusAsync(id, "Đã hủy");
        }
        public Task<Bill> UpdateWithShipperAsync(int id)
        {
            return UpdateBillStatusAsync(id, "Đang giao");
        }

        public Task<Bill> UpdateWithShipper2Async(int id)
        {
            return UpdateBillStatusAsync(id, "Đã giao");
        }
    }
}
