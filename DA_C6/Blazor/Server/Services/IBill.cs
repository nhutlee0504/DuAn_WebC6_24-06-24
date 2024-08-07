using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blazor.Server.Services
{
    public interface IBill
    {
        public IEnumerable<Bill> GetAllBill();

        Task<IEnumerable<Bill>> GetUserBillsAsync(string username, int pageNumber, int pageSize);

        public Bill GetBillId(int id);
        Task<bool> PayAsync(string username, List<int> selectedProducts, decimal totalPrice);
        Task<bool> CancelBillAsync(int billId);
        Task<bool> UpdateBillStatusAsync(int billId, string newStatus);
        Task<bool> UpdateBillStatusAsync2(int billId);
    }
}
