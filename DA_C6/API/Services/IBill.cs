using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IBill
    {
        public IEnumerable<Bill> GetAllBill();

        public Bill GetBillId(int id);
        Task<IEnumerable<Bill>> GetUserBillsAsync(string username, int pageNumber, int pageSize);
    }
}
