﻿using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
{
    public class BillDetailResponse : IBillDetail
    {
        private readonly ApplicationDbContext _context;

        public List<BillDetails> GetAllBillDetails()
        {
            return _context.BillDetails.ToList();
        }

        public List<BillDetails> GetBillDetails(int id)
        {
            return _context.BillDetails
                           .Include(bd => bd.ProductDetails)
                               .ThenInclude(pd => pd.Product)
                           .Include(bd => bd.ProductDetails)
                               .ThenInclude(pd => pd.Colors)
                           .Include(bd => bd.ProductDetails)
                               .ThenInclude(pd => pd.Sizes)
                           .Where(x => x.IDBill == id)
                           .ToList();
        }

        public IEnumerable<BillDetails> GetBillDetailsForAdmin()
        {
            return _context.BillDetails;
        }
    }
}
