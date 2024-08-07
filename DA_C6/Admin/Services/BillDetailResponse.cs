using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Admin.Services
{
    public class BillDetailResponse : IBillDetail
    {
        private readonly ApplicationDbContext _context;

        public BillDetailResponse(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bill> GetBillDetailsAsync(int id)
        {
            return await _context.Bills
                .Include(b => b.BillDetails)
                .ThenInclude(bd => bd.ProductDetails)
                .ThenInclude(pd => pd.Product)
                .FirstOrDefaultAsync(b => b.IDBill == id);
        }

        public IEnumerable<BillDetails> GetBillDetailsForAdmin(int id)
        {
            return _context.BillDetails.Where(x => x.IDBill == id);
        }
    }
}
