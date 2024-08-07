using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace Blazor.Server.Services
{
    public class BillResponse : IBill
    {
        private readonly ApplicationDbContext _context;
        public BillResponse(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> PayAsync(string username, List<int> selectedProducts, decimal totalPrice)
        {
            var selectedCartItems = await _context.Carts
                .Where(x => x.UserName == username && selectedProducts.Contains(x.IDPDetail))
                .Include(x => x.ProductDetails.Product)
                .Include(x => x.ProductDetails.Product.Category)
                .Include(x => x.ProductDetails.Sizes)
                .Include(x => x.ProductDetails.Colors)
                .ToListAsync();

            if (selectedCartItems == null || !selectedCartItems.Any())
            {
                return false;
            }

            var addbill = new Bill
            {
                UserName = username,
                OrderDate = DateTime.Now,
                TotalAmount = totalPrice,
                Status = "Đang chờ"
            };
            _context.Bills.Add(addbill);
            await _context.SaveChangesAsync();

            int billId = addbill.IDBill;
            foreach (var cartItem in selectedCartItems)
            {
                var billDetail = new BillDetails
                {
                    IDBill = billId,
                    IDPDetail = cartItem.IDPDetail,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.ProductDetails.Product.Price * cartItem.Quantity
                };
                _context.BillDetails.Add(billDetail);

                var pd = await _context.ProductDetails.FirstOrDefaultAsync(p => p.IDPDetail == cartItem.IDPDetail);
                if (pd != null)
                {
                    pd.Quantity -= cartItem.Quantity;

                    if (pd.Quantity < 0)
                    {
                        pd.Quantity = 0;
                    }

                    _context.ProductDetails.Update(pd);
                }

                _context.Carts.Remove(cartItem);
            }

            var hs = new History
            {
                UserName = username,
                Describe = "Đơn hàng của " + username + " đã được tạo"
            };
            _context.Histories.Add(hs);

            await _context.SaveChangesAsync();

            return true;
        }
        public IEnumerable<Bill> GetAllBill()
        {
            return _context.Bills;
        }

        public Bill GetBillId(int id)
        {
            return _context.Bills.FirstOrDefault(x => x.IDBill == id);
        }

        public async Task<IEnumerable<Bill>> GetUserBillsAsync(string username, int pageNumber, int pageSize)
        {
            return await _context.Bills
                .Where(b => b.UserName == username)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<bool> CancelBillAsync(int billId)
        {
            var bill = await _context.Bills.FindAsync(billId);
            if (bill != null && bill.Status == "Đang chờ")
            {
                bill.Status = "Đã hủy";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
  
        public async Task<bool> UpdateBillStatusAsync(int billId, string newStatus)
        {
            var bill = await _context.Bills.FindAsync(billId);
            if (bill != null)
            {
                bill.Status = newStatus;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateBillStatusAsync2(int billId)
        {
            var bill = await _context.Bills.FindAsync(billId);
            if (bill != null)
            {
                bill.Status = "Hoàn thành";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
