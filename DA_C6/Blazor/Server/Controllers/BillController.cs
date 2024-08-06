using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blazor.Shared.Model;
using Blazor.Server.Data;
using Blazor.Server.Services;
using System; // Giả sử DbContext nằm trong namespace này

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Đảm bảo sử dụng DbContext của bạn
        private readonly IBill _bill;

        public BillController(ApplicationDbContext context, IBill bill)
        {
            _context = context;
            _bill = bill;
        }

        [HttpGet]
        public IEnumerable<Bill> GetSizes()
        {
            return _bill.GetAllBill();
        }

        [HttpGet("{id}")]
        public Bill GetBillId(int id)
        {
            return _bill.GetBillId(id);
        }
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResponse<Bill>>> GetUserBillsPaged(string username, int pageNumber = 1, int pageSize = 4)
        {
            var totalBills = await _context.Bills.CountAsync(b => b.UserName == username);
            var bills = await _context.Bills
                .Where(b => b.UserName == username)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (bills == null || !bills.Any())
            {
                return NotFound();
            }

            var totalPages = (int)Math.Ceiling(totalBills / (double)pageSize);
            var response = new PagedResponse<Bill>
            {
                Data = bills,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };

            return Ok(response);
        }

        public class PagedResponse<T>
        {
            public IEnumerable<T> Data { get; set; }
            public int TotalPages { get; set; }
            public int CurrentPage { get; set; }
        }
    }
}


