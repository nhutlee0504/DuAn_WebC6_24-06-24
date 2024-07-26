using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public interface ISale
    {
        public IEnumerable<Sale> GetSale();

        public Sale AddSale(Sale sale);
        public Sale GetSaleByID(int id);
        public Sale GetSaleByName (string name);
    }
}
