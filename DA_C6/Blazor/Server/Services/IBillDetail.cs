using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public interface IBillDetail
    {
        public List<BillDetails> GetBillDetails(int id);
        public IEnumerable<BillDetails> GetBillDetailsForAdmin(int id);
    }
}
