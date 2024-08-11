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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BillController(ApplicationDbContext context, IBill bill, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _bill = bill;
            _httpContextAccessor = httpContextAccessor;
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
          
            var totalPages = (int)Math.Ceiling(totalBills / (double)pageSize);
            var response = new PagedResponse<Bill>
            {
                Data = bills,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };

            return Ok(response);
        }
        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PaymentRequest request)
        {
            if (string.IsNullOrEmpty(request.Username))
            {
                return Unauthorized();
            }

            bool result = await _bill.PayAsync(request.Username, request.SelectedProducts, request.TotalPrice);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Payment failed.");
            }
        }
        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelBill(int id)
        {
            bool result = await _bill.CancelBillAsync(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Không thể hủy đơn hàng này.");
            }
        }

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateBillStatus([FromBody] UpdateBillStatusRequest request)
        {
            bool result = await _bill.UpdateBillStatusAsync(request.BillId, request.NewStatus);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Không thể cập nhật trạng thái đơn hàng này.");
            }
        }
        [HttpPost("update-status2/{id}")]

        public async Task<IActionResult> UpdateBillStatus2(int id)
        {
            bool result = await _bill.UpdateBillStatusAsync2(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Không thể cập nhật trạng thái đơn hàng này.");
            }
        }

        [HttpGet]
        [Route("GetAllBills")]
        public IEnumerable<Bill> GetAllBills()
        {
            return _bill.GetAllBill();
        }

        public class UpdateBillStatusRequest
        {
            public int BillId { get; set; }
            public string NewStatus { get; set; }
        }
        public class PaymentRequest
        {
            public string Username { get; set; }
            public List<int> SelectedProducts { get; set; }
            public decimal TotalPrice { get; set; }
        }
        public class PagedResponse<T>
        {
            public IEnumerable<T> Data { get; set; }
            public int TotalPages { get; set; }
            public int CurrentPage { get; set; }
        }
    }
}


