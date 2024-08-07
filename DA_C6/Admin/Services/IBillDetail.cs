using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Admin.Services
{
    public interface IBillDetail
    {
        public Task<Bill> GetBillDetailsAsync(int id);

        public IEnumerable<BillDetails> GetBillDetailsForAdmin(int id);
    }
}
