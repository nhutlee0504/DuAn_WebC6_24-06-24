using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Blazor.Shared.Model;
namespace Blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailController : ControllerBase
    {
        private IBillDetail bid;
        public BillDetailController(IBillDetail s) => bid = s;
		[HttpGet("details/{id}")]
		public IActionResult GetBillDetails(int id)
        {
            var billDetails = bid.GetBillDetails(id);
            if (billDetails == null)
            {
                return NotFound();
            }

            try
            {
                var viewModel = billDetails.Select(bd => new
                {
                    bd.IDBDetail,
                    bd.IDBill,
                    bd.IDPDetail,
                    bd.Quantity,
                    bd.Price,
                    Product = new
                    {
                        IDProduct = bd.ProductDetails != null ? bd.ProductDetails.IDProduct : 0,
                        Name = bd.ProductDetails != null && bd.ProductDetails.Product != null ? bd.ProductDetails.Product.Name : null,
                        Image = bd.ProductDetails != null && bd.ProductDetails.Product != null ? bd.ProductDetails.Product.Image : null,
                        Color = bd.ProductDetails != null && bd.ProductDetails.Colors != null ? bd.ProductDetails.Colors.Color : null,
                        Size = bd.ProductDetails != null && bd.ProductDetails.Sizes != null ? bd.ProductDetails.Sizes.SizeName : null
                    }
                });

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet]
        public IEnumerable<BillDetails> GetBDID(int id)
        {
            return bid.GetBillDetailsForAdmin(id);
        }

    }
}
