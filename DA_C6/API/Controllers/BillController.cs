using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private IBill bill;
        public BillController(IBill s) => bill = s;

        [HttpGet]
        public IEnumerable<Bill> GetSizes()
        {
            return bill.GetAllBill();
        }

        [HttpGet("{id}")]
        public Bill GetBillId(int id)
        {
            return bill.GetBillId(id);
        }
        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PaymentRequest request)
        {
            string username = HttpContext.Session.GetString("LoggedInUser");

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            bool result = await bill.PayAsync(username, request.SelectedProducts, request.TotalPrice);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Payment failed.");
            }
        }
        public class PaymentRequest
        {
            public List<int> SelectedProducts { get; set; }
            public decimal TotalPrice { get; set; }
        }
    }
}
