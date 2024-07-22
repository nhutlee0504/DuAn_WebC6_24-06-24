using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public interface IBillDetail
    {
        public List<BillDetails> GetBillDetails(int id);
        public IEnumerable<BillDetails> GetBillDetailsForAdmin(int id);
    }
}
