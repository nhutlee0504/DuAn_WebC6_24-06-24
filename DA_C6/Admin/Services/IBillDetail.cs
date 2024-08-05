using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services
{
    public interface IBillDetail
    {
        public List<BillDetails> GetBillDetails(int id);
        public IEnumerable<BillDetails> GetBillDetailsForAdmin(int id);
    }
}
