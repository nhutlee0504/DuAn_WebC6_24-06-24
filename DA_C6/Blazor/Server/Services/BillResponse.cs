using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Blazor.Server.Services
{
    public class BillResponse : IBill
    {
        private readonly HttpClient _httpClient;

        public BillResponse(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Bill>> GetUserBillsAsync(string username, int pageNumber, int pageSize)
        {
            return await _context.Bills
                .Where(b => b.UserName == username)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        private readonly ApplicationDbContext _context;
        public BillResponse(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Bill> GetAllBill()
        {
            return _context.Bills;
        }

        public Bill GetBillId(int id)
        {
            return _context.Bills.FirstOrDefault(x => x.IDBill == id);
        }
    }
}
