using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using API.Services;
using System.Collections.Generic;

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
    }
}
